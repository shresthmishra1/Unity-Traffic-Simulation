using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;
    private bool isRunning;
    private readonly Queue<Action> mainThreadActions = new Queue<Action>();
    // private int greenDuration = 45;
    private static int greenDuration = 45;
    private float yellowDuration = 3f;

    // Use phasenum to track which phase is active:
    // Phase 0: lights 0 & 1 are green and lights 2 & 3 are red.
    // Phase 1: lights 0 & 1 are red and lights 2 & 3 are green.
    public int phasenum = 0; 

    public int lightSwitchCount = 0;
    public Boolean optimized = true;

    public Boolean realVals = true;

    private float prevSwitchTime;
    private float currTime;
    private float currPhaseDurationSofar;

    // # small config
    //  MIN_PHASE_DURATION = 5
    //  MAX_PHASE_DURATION = 15
    
    //  medium config
    float MIN_PHASE_DURATION = (float) greenDuration/3f;
    float MAX_PHASE_DURATION = (float) greenDuration;

    // large config
    // MIN_PHASE_DURATION = 15
    // MAX_PHASE_DURATION = 45  


    float RATIO_THRESHOLD = 2;
    float ABS_DIFF_THRESHOLD = 7;
    
    public int GetGreenDuration()
    {
        return greenDuration;
    }
    void Start()
    {
        // Set the initial state of the lights.
        prevSwitchTime = Time.time;
        SetInitialTrafficLights();
        // Debug.Log(optimized);
        // Debug.Log(realVals);
        if(optimized && !realVals)
        {
            StartServer();
        }
        // else if(optimized && realVals)
        // {
        //     Debug.Log("LALALALA");
        //     StartCoroutine(RealVals());
        // }
        else if(!optimized)
        {
            Debug.Log("KFWEKAFKWEKLFWELKKLFWEKLKLFKLKLKLKKLKLKLKLKLKWKELFJEFEWFEWFEWFEWFEWFEWFWEFEWFEWFEWFWEFEWFEWFWE");
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
        currTime = Time.time;
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();
        GameObject mainObj = GameObject.FindGameObjectWithTag("MainCamera");
        main mainscript = mainObj.GetComponent<main>();

        mainscript.upLeftStats["green time"] = sortedLightList[0].greenTime;
        mainscript.downLeftStats["green time"] = sortedLightList[1].greenTime;
        mainscript.upStats["green time"] = sortedLightList[2].greenTime;
        mainscript.downStats["green time"] = sortedLightList[3].greenTime;
        mainscript.rightLeftStats["green time"] = sortedLightList[4].greenTime;
        mainscript.leftLeftStats["green time"] = sortedLightList[5].greenTime;
        mainscript.rightStats["green time"] = sortedLightList[6].greenTime;
        mainscript.leftStats["green time"] = sortedLightList[7].greenTime;

        if(optimized && realVals)
        {
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            int upCars = 0;
            int downCars = 0;
            int leftCars = 0;
            int rightCars = 0;
            int upLeftCars = 0;
            int downLeftCars = 0;
            int leftLeftCars = 0;
            int rightLeftCars = 0;
            foreach (GameObject obj in objects)
            {
                if (obj.name == "car_up")
                {
                    upCars++;
                }
                else if (obj.name == "car_down")
                {
                    downCars++;
                }
                else if(obj.name == "car_left")
                {
                    leftCars++;
                }
                else if(obj.name == "car_right")
                {
                    rightCars++;
                }
                else if (obj.name == "car_upLeft")
                {
                    upLeftCars++;
                }
                else if (obj.name == "car_downLeft")
                {
                    downLeftCars++;
                }
                else if(obj.name == "car_leftLeft")
                {
                    leftLeftCars++;
                }
                else if(obj.name == "car_rightLeft")
                {
                    rightLeftCars++;
                }
            }
            // currTime = Time.time;
            currPhaseDurationSofar = currTime - prevSwitchTime;
            if(MAX_PHASE_DURATION < currPhaseDurationSofar)
            {
                Debug.Log("LALALALA");
                phasenum = (phasenum + 1) % 4;
                prevSwitchTime = currTime;
                StartCoroutine(SwitchTrafficLights(phasenum));
            }
            else if(currPhaseDurationSofar < MIN_PHASE_DURATION)
            {
            }
            else if(sortedLightList[0].isGreenLight() && sortedLightList[1].isGreenLight())
            {
                Debug.Log("LALALALA");
                if((upCars + downCars) >= RATIO_THRESHOLD * (upLeftCars + downLeftCars) || (upCars + downCars) - (downLeftCars + upLeftCars) >= ABS_DIFF_THRESHOLD)
                {
                    phasenum = (phasenum + 1) % 4;
                    prevSwitchTime = currTime;
                    StartCoroutine(SwitchTrafficLights(phasenum));
                }
            }
            else if(sortedLightList[2].isGreenLight() && sortedLightList[3].isGreenLight())
            {
                if((leftLeftCars + rightLeftCars) >= RATIO_THRESHOLD * (upCars + downCars) || (leftLeftCars + rightLeftCars) - (upCars + downCars) >= ABS_DIFF_THRESHOLD)
                {
                    phasenum = (phasenum + 1) % 4;
                    prevSwitchTime = currTime;
                    StartCoroutine(SwitchTrafficLights(phasenum));
                }
            }
            else if(sortedLightList[4].isGreenLight() && sortedLightList[5].isGreenLight())
            {
                if((leftCars + rightCars) >= RATIO_THRESHOLD * (leftLeftCars + rightLeftCars) || (leftCars + rightCars) - (rightLeftCars + leftLeftCars) >= ABS_DIFF_THRESHOLD)
                {
                    phasenum = (phasenum + 1) % 4;
                    prevSwitchTime = currTime;
                    StartCoroutine(SwitchTrafficLights(phasenum));
                }
            }
            else if(sortedLightList[6].isGreenLight() && sortedLightList[7].isGreenLight())
            {
                if((upLeftCars + downLeftCars) >= RATIO_THRESHOLD * (leftCars + rightCars) || (upLeftCars + rightCars) - (leftCars + rightCars) >= ABS_DIFF_THRESHOLD)
                {
                    phasenum = (phasenum + 1) % 4;
                    prevSwitchTime = currTime;
                    StartCoroutine(SwitchTrafficLights(phasenum));
                }
            }
        }

    }

    IEnumerator RealVals()
    {

        while(true)
        {
            
        }
        
    }

    IEnumerator NonOptimized()
    {
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();
        GameObject mainObj = GameObject.FindGameObjectWithTag("MainCamera");
        main mainscript = mainObj.GetComponent<main>();
        bool timeup = mainscript.isTimerComplete;
        // Debug.Log("KFWEKAFKWEKLFWELKKLFWEKLKLFKLKLKLKKLKLKLKLKLKWKELFJEFEWFEWFEWFEWFEWFEWFWEFEWFEWFEWFWEFEWFEWFWE");
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
                if(!mainscript.isTimerComplete)
                {
                    lightSwitchCount += 1;
                }
                Debug.Log("FIRST PHASE IS OVER");
                sortedLightList[2].lightPhase = 0;
                sortedLightList[3].lightPhase = 0;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[2].lightPhase = 1;
                sortedLightList[3].lightPhase = 1;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[2].lightPhase = 2;
                sortedLightList[3].lightPhase = 2;
                if(!mainscript.isTimerComplete)
                {
                    lightSwitchCount += 1;
                }
                
                sortedLightList[4].lightPhase = 0;
                sortedLightList[5].lightPhase = 0;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[4].lightPhase = 1;
                sortedLightList[5].lightPhase = 1;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[4].lightPhase = 2;
                sortedLightList[5].lightPhase = 2;
                if(!mainscript.isTimerComplete)
                {
                    lightSwitchCount += 1;
                }
                                
                sortedLightList[6].lightPhase = 0;
                sortedLightList[7].lightPhase = 0;
                yield return new WaitForSeconds(greenDuration);
                sortedLightList[6].lightPhase = 1;
                sortedLightList[7].lightPhase = 1;
                yield return new WaitForSeconds(yellowDuration);
                sortedLightList[6].lightPhase = 2;
                sortedLightList[7].lightPhase = 2;
                if(!mainscript.isTimerComplete)
                {
                    lightSwitchCount += 1;
                }
            // sortedLightList[0].lightPhase = 2;
            // sortedLightList[1].lightPhase = 2;
            // sortedLightList[2].lightPhase = 2;
            // sortedLightList[3].lightPhase = 2;
            // yield return new WaitForSeconds(yellowDuration);
            // sortedLightList[4].lightPhase = 2;
            // sortedLightList[5].lightPhase = 2;
            // sortedLightList[6].lightPhase = 2;
            // sortedLightList[7].lightPhase = 2;
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
                phasenum = (phasenum + 1) % 4;

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
            sortedLightList[4].lightPhase = 2;
            sortedLightList[5].lightPhase = 2;
            sortedLightList[6].lightPhase = 2;
            sortedLightList[7].lightPhase = 2;
        }
        else if (phasenum == 1)
        {
            // Phase 1: Lights 0 & 1 red, 2 & 3 green.
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;
            sortedLightList[2].lightPhase = 0;
            sortedLightList[3].lightPhase = 0;
            sortedLightList[4].lightPhase = 2;
            sortedLightList[5].lightPhase = 2;
            sortedLightList[6].lightPhase = 2;
            sortedLightList[7].lightPhase = 2;
        }
        else if (phasenum == 2)
        {
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;
            sortedLightList[4].lightPhase = 0;
            sortedLightList[5].lightPhase = 0;
            sortedLightList[6].lightPhase = 2;
            sortedLightList[7].lightPhase = 2;
        }
        else if (phasenum == 3)
        {
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;
            sortedLightList[4].lightPhase = 2;
            sortedLightList[5].lightPhase = 2;
            sortedLightList[6].lightPhase = 0;
            sortedLightList[7].lightPhase = 0;
        }
        // sortedLightList[0].lightPhase = 2;
        // sortedLightList[1].lightPhase = 2;
        //     sortedLightList[2].lightPhase = 2;
        //     sortedLightList[3].lightPhase = 2;
        //     sortedLightList[4].lightPhase = 2;
        //     sortedLightList[5].lightPhase = 2;
        //     sortedLightList[6].lightPhase = 2;
        //     sortedLightList[7].lightPhase = 2;
        // Debug.Log("Initial traffic lights set.");
    }

    // Coroutine that handles switching traffic lights with a yellow transition.
    IEnumerator SwitchTrafficLights(int newPhase)
    {

        GameObject mainObj = GameObject.FindGameObjectWithTag("MainCamera");
        main mainscript = mainObj.GetComponent<main>();
        bool timeup = mainscript.isTimerComplete;


        var lightList = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = lightList.OrderBy(light => light.light_number).ToList();

        if (newPhase == 0)
        {
            // In phase 0, lights 2 and 3 will become red.
            // First, set them to yellow.
            if(!timeup)
            {
                lightSwitchCount += 1;
            }
            
            sortedLightList[6].lightPhase = 1;
            sortedLightList[7].lightPhase = 1;
            

            // Ensure lights 0 and 1 remain green (or at least not red/yellow).
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;

            Debug.Log("Phase 0 transition: Lights 2 and 3 set to yellow.");
            yield return new WaitForSeconds(yellowDuration);

            // After 1 second, switch lights 2 and 3 to red.
            sortedLightList[6].lightPhase = 2;
            sortedLightList[7].lightPhase = 2;


            sortedLightList[0].lightPhase = 0;
            sortedLightList[1].lightPhase = 0;
            Debug.Log("Phase 0 transition: Lights 2 and 3 are now red.");
        }
        else if (newPhase == 1)
        {
            // In phase 1, lights 0 and 1 will become red.
            // First, set them to yellow.
            if(!timeup)
            {
                lightSwitchCount += 1;
            }
            sortedLightList[0].lightPhase = 1;
            sortedLightList[1].lightPhase = 1;

            // Ensure lights 2 and 3 remain red
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;

            Debug.Log("Phase 1 transition: Lights 0 and 1 set to yellow.");
            yield return new WaitForSeconds(yellowDuration);

            // set lights 2 and 3 to green
            sortedLightList[2].lightPhase = 0;
            sortedLightList[3].lightPhase = 0;
            
            // After 1 second, switch lights 0 and 1 to red.
            sortedLightList[0].lightPhase = 2;
            sortedLightList[1].lightPhase = 2;
            Debug.Log("Phase 1 transition: Lights 0 and 1 are now red.");
        }
        else if (newPhase == 2)
        {
            // In phase 1, lights 0 and 1 will become red.
            // First, set them to yellow.
            if(!timeup)
            {
                lightSwitchCount += 1;
            }
            sortedLightList[2].lightPhase = 1;
            sortedLightList[3].lightPhase = 1;

            // Ensure lights 2 and 3 remain red
            sortedLightList[4].lightPhase = 2;
            sortedLightList[5].lightPhase = 2;

            Debug.Log("Phase 1 transition: Lights 0 and 1 set to yellow.");
            yield return new WaitForSeconds(yellowDuration);

            // set lights 2 and 3 to green
            sortedLightList[4].lightPhase = 0;
            sortedLightList[5].lightPhase = 0;
            
            // After 1 second, switch lights 0 and 1 to red.
            sortedLightList[2].lightPhase = 2;
            sortedLightList[3].lightPhase = 2;
            Debug.Log("Phase 1 transition: Lights 0 and 1 are now red.");
        }
        else if (newPhase == 3)
        {
            // In phase 1, lights 0 and 1 will become red.
            // First, set them to yellow.
            if(!timeup)
            {
                lightSwitchCount += 1;
            }
            sortedLightList[4].lightPhase = 1;
            sortedLightList[5].lightPhase = 1;

            // Ensure lights 2 and 3 remain red
            sortedLightList[6].lightPhase = 2;
            sortedLightList[7].lightPhase = 2;

            Debug.Log("Phase 1 transition: Lights 0 and 1 set to yellow.");
            yield return new WaitForSeconds(yellowDuration);

            // set lights 2 and 3 to green
            sortedLightList[6].lightPhase = 0;
            sortedLightList[7].lightPhase = 0;
            
            // After 1 second, switch lights 0 and 1 to red.
            sortedLightList[4].lightPhase = 2;
            sortedLightList[5].lightPhase = 2;
            Debug.Log("Phase 1 transition: Lights 0 and 1 are now red.");
        }
        Debug.Log("Traffic lights updated!");
    }
}
