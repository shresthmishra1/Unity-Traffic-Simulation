using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TrafficLightManager : MonoBehaviour
{
    private TcpListener server;
    private bool isRunning;
    public int greenDuration = 2;
    public int yellowDuration = 1;
    private Thread serverThread;
    Boolean updowngreen = false;

    
    void Start()
    {
        ManageTrafficLights();
        StartServer();
        
    }


    void StartServer()
    {
        try
        {
            int port = 8080; // Same port as the client
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isRunning = true;
            Debug.Log("Server started, waiting for connections...");

            // Start listening for connections in a separate thread
            server.BeginAcceptTcpClient(OnClientConnected, server);
        }
        catch (Exception e)
        {
            Debug.LogError($"Server error: {e.Message}");
        }
    }

    void OnClientConnected(IAsyncResult result)
    {
        TcpClient client = server.EndAcceptTcpClient(result);
        Debug.Log("Client connected!");

        // Handle client communication
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);

        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Debug.Log($"Received: {message}");

        // Update the traffic light based on the message
        if (message.StartsWith("PHASE_CHANGE"))
        {
            Debug.Log("Updating traffic light phase...");
            updowngreen = !updowngreen;
            ManageTrafficLights();
            // Add your Unity logic here for phase changes
        }

        client.Close();

        // Continue accepting new connections
        server.BeginAcceptTcpClient(OnClientConnected, server);
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        server?.Stop();
    }
    void ManageTrafficLights()
    {
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();
        
    
        if(updowngreen)
        {
            sortedLightList[0].isRed = false;
            sortedLightList[1].isRed = false;
            sortedLightList[2].isRed = true;
            sortedLightList[3].isRed = true;
        }
        else
        {
            sortedLightList[0].isRed = false;
            sortedLightList[1].isRed = false;
            sortedLightList[2].isRed = true;
            sortedLightList[3].isRed = true;
        }
            // sortedLightList[0].isRed = false;
            // sortedLightList[1].isRed = false;
            // yield return new WaitForSeconds(greenDuration);
            // sortedLightList[0].isYellow = true;
            // sortedLightList[1].isYellow = true;
            // yield return new WaitForSeconds(yellowDuration);
            // sortedLightList[0].isYellow = false;
            // sortedLightList[0].isRed = true;
            // sortedLightList[1].isYellow = false;
            // sortedLightList[1].isRed = true;

            // sortedLightList[2].isRed = false;
            // sortedLightList[3].isRed = false;
            // yield return new WaitForSeconds(greenDuration);
            // sortedLightList[2].isYellow = true;
            // sortedLightList[3].isYellow = true;
            // yield return new WaitForSeconds(yellowDuration);
            // sortedLightList[2].isYellow = false;
            // sortedLightList[2].isRed = true;
            // sortedLightList[3].isYellow = false;
            // sortedLightList[3].isRed = true;
            
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
