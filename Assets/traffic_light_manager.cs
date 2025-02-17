using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;
    private bool isRunning;
    private readonly Queue<Action> mainThreadActions = new Queue<Action>();
    public int greenDuration = 2;
    public int yellowDuration = 1;

    // Use phasenum to track which phase is active:
    // Phase 0: lights 0 & 1 are green and lights 2 & 3 are red.
    // Phase 1: lights 0 & 1 are red and lights 2 & 3 are green.
    public int phasenum = 0; 

    public Boolean optimized = false;
    void Start()
    {
        // Set the initial state of the lights.
        SetInitialTrafficLights();
        if(optimized)
        {
            StartServer();
        }
        else
        {
            StartCoroutine(NonOptimized());
        }
        
    }

    void Update()
    {
        // Process any actions queued from background threads.
        lock (mainThreadActions)
        {
            while (mainThreadActions.Count > 0)
            {
                mainThreadActions.Dequeue()?.Invoke();
            }
        }
    }


    IEnumerator NonOptimized()
    {
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();

        while (true)
        {
                sortedLightList[0].lightPhase = 0;
                sortedLightList[1].lightPhase = 0;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[0].lightPhase = 1;
                sortedLightList[1].lightPhase = 1;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[0].lightPhase = 2;
                sortedLightList[1].lightPhase = 2;

                sortedLightList[2].lightPhase = 0;
                sortedLightList[3].lightPhase = 0;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[2].lightPhase = 1;
                sortedLightList[3].lightPhase = 1;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[2].lightPhase = 2;
                sortedLightList[3].lightPhase = 2;
                
                // sortedLightList[4].isRed = false;
                // sortedLightList[5].isRed = false;
                // yield return new WaitForSeconds(greenDuration);
                // sortedLightList[4].isYellow = true;
                // sortedLightList[5].isYellow = true;
                // yield return new WaitForSeconds(yellowDuration);
                // sortedLightList[4].isYellow = false;
                // sortedLightList[4].isRed = true;
                // sortedLightList[5].isYellow = false;
                // sortedLightList[5].isRed = true;
                                
                // sortedLightList[6].isRed = false;
                // sortedLightList[7].isRed = false;
                // yield return new WaitForSeconds(greenDuration);
                // sortedLightList[6].isYellow = true;
                // sortedLightList[7].isYellow = true;
                // yield return new WaitForSeconds(yellowDuration);
                // sortedLightList[6].isYellow = false;
                // sortedLightList[6].isRed = true;
                // sortedLightList[7].isYellow = false;
                // sortedLightList[7].isRed = true;
        }
    }

    void StartServer()
    {
        try
        {
            int port = 8080;
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isRunning = true;
            Debug.Log("Server started, waiting for connections...");

            serverThread = new Thread(ServerLoop);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError($"Server error: {e.Message}");
        }
    }

    void ServerLoop()
    {
        while (isRunning)
        {
            if (!server.Pending())
            {
                Thread.Sleep(100); // Avoid busy waiting
                continue;
            }

            TcpClient client = server.AcceptTcpClient();
            Debug.Log("Client connected!");
            HandleClient(client);
        }
    }

    void HandleClient(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Debug.Log($"Received: {message}");

            if (message.StartsWith("PHASE_CHANGE"))
            {
                Debug.Log("Received phase change command.");
                // Toggle the phase. For example, if phase was 0, change to 1.
                phasenum = (phasenum + 1) % 2;

                // Enqueue a lambda to start the traffic light transition coroutine on the main thread.
                lock (mainThreadActions)
                {
                    mainThreadActions.Enqueue(() => StartCoroutine(SwitchTrafficLights(phasenum)));
                }
            }
            
            // Optional: send acknowledgment back to the client.
            string response = "ACK: Phase updated";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            stream.Write(responseData, 0, responseData.Length);
        }
        catch (Exception e)
        {
            Debug.LogError($"Client handling error: {e.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        server?.Stop();
        serverThread?.Join(); // Ensure the server thread terminates properly.
    }

    // Sets the initial state of the traffic lights based on the current phase.
    void SetInitialTrafficLights()
    {
        var lightList = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = lightList.OrderBy(light => light.light_number).ToList();

        if (phasenum == 0)
        {
            // Phase 0: Lights 0 & 1 green, 2 & 3 red.
            sortedLightList[0].lightPhase = 0;
            sortedLightList[1].lightPhase = 0;
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;
        }
        else if (phasenum == 1)
        {
            // Phase 1: Lights 0 & 1 red, 2 & 3 green.
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;
            sortedLightList[2].lightPhase = 0;
            sortedLightList[3].lightPhase = 0;
        }

        Debug.Log("Initial traffic lights set.");
    }

    // Coroutine that handles switching traffic lights with a yellow transition.
    IEnumerator SwitchTrafficLights(int newPhase)
    {
        var lightList = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = lightList.OrderBy(light => light.light_number).ToList();

        if (newPhase == 0)
        {
            // In phase 0, lights 2 and 3 will become red.
            // First, set them to yellow.
            sortedLightList[2].lightPhase = 1;
            sortedLightList[3].lightPhase = 1;
            

            // Ensure lights 0 and 1 remain green (or at least not red/yellow).
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;

            Debug.Log("Phase 0 transition: Lights 2 and 3 set to yellow.");
            yield return new WaitForSeconds(5.0f);

            // After 1 second, switch lights 2 and 3 to red.
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;


            sortedLightList[0].lightPhase = 0;
            sortedLightList[1].lightPhase = 0;
            Debug.Log("Phase 0 transition: Lights 2 and 3 are now red.");
        }
        else if (newPhase == 1)
        {
            // In phase 1, lights 0 and 1 will become red.
            // First, set them to yellow.
            sortedLightList[0].lightPhase = 1;
            sortedLightList[1].lightPhase = 1;

            // Ensure lights 2 and 3 remain red
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;

            Debug.Log("Phase 1 transition: Lights 0 and 1 set to yellow.");
            yield return new WaitForSeconds(5.0f);

            // set lights 2 and 3 to green
            sortedLightList[2].lightPhase = 0;
            sortedLightList[3].lightPhase = 0;
            
            // After 1 second, switch lights 0 and 1 to red.
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;
            Debug.Log("Phase 1 transition: Lights 0 and 1 are now red.");
        }
        Debug.Log("Traffic lights updated!");
    }
}
