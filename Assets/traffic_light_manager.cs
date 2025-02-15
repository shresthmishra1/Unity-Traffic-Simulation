using System;
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
    private bool updowngreen = false;

    void Start()
    {
        ManageTrafficLights();
        StartServer();
    }

    void Update()
    {
        // Process queued main thread actions
        lock (mainThreadActions)
        {
            while (mainThreadActions.Count > 0)
            {
                mainThreadActions.Dequeue()?.Invoke();
            }
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
                Debug.Log("Updating traffic light phase...");
                updowngreen = !updowngreen;

                // Queue the traffic light update back to the main thread
                lock (mainThreadActions)
                {
                    mainThreadActions.Enqueue(ManageTrafficLights);
                }
            }
            
            // Optional: send acknowledgment back to the client
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
        serverThread?.Join(); // Ensure the server thread terminates properly
    }

    void ManageTrafficLights()
    {
        var lightList = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = lightList.OrderBy(light => light.light_number).ToList();

        if (updowngreen)
        {
            sortedLightList[0].isRed = false;
            sortedLightList[1].isRed = false;
            sortedLightList[2].isRed = true;
            sortedLightList[3].isRed = true;
        }
        else
        {
            sortedLightList[0].isRed = true;
            sortedLightList[1].isRed = true;
            sortedLightList[2].isRed = false;
            sortedLightList[3].isRed = false;
        }

        Debug.Log("Traffic lights updated!");
    }
}
