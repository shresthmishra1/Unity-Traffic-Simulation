using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;
using JetBrains.Annotations;
using System;
using Mono.Cecil.Cil;

public class main : MonoBehaviour
{
    double up = 0;
    double down = 0;
    double left = 0;
    double right = 0;
    double upLeft = 0;
    double downLeft = 0;
    double leftLeft = 0;
    double rightLeft = 0;
    double upRight = 0;
    double downRight = 0;
    double leftRight = 0;
    double rightRight = 0;

// config 1
    // static double var1 = 1;
    // static double var2 = 1;
    // static double var3 = 1;
    // static double var4 = 1;
// config 2
    // static double var1 = 1.5;
    // static double var2 = 1.5;
    // static double var3 = 1;
    // static double var4 = 1;
// config 3
    static double var1 = 3;
    static double var2 = 3;
    static double var3 = 1;
    static double var4 = 1;
// config 4
    // static double var1 = 1.5;
    // static double var2 = 1;
    // static double var3 = 1;
    // static double var4 = 1;
// config 5
    // double var1 = 3;
    // double var2 = 1;
    // double var3 = 1;
    // double var4 = 1;

    // static double sizeCoeff = 0.185; // small
    // static double sizeCoeff = 0.37; // medium
    // public static double sizeCoeff = 0.56; // large
    public static double sizeCoeff;


    double upSpawn;
    double downSpawn;
    double leftSpawn;  
    double rightSpawn;
    double upLeftSpawn;
    double downLeftSpawn;
    double leftLeftSpawn;
    double rightLeftSpawn;
    double upRightSpawn;
    double downRightSpawn;
    double leftRightSpawn;
    double rightRightSpawn;

    


    double upBike = 0;
    double downBike = 0;
    double leftBike = 0;
    double rightBike = 0;
    double upLeftBike = 0;
    double downLeftBike = 0;
    double leftLeftBike = 0;
    double rightLeftBike = 0;
    double upRightBike = 0;
    double downRightBike = 0;
    double leftRightBike = 0;
    double rightRightBike = 0;

    double upBikeSpawn = 0;
    double downBikeSpawn = 0;
    double leftBikeSpawn = 0;
    double rightBikeSpawn = 0;
    double upLeftBikeSpawn = 0;
    double downLeftBikeSpawn = 0;
    double leftLeftBikeSpawn = 0;
    double rightLeftBikeSpawn = 0;
    double upRightBikeSpawn = 0;
    double downRightBikeSpawn = 0;
    double leftRightBikeSpawn = 0;
    double rightRightBikeSpawn = 0;
    double frameCount = 0;

    public int lastRecordedSecond = -1;
    public int congestedCars = 0;

    private string[] bikeColors = { "orange_bike", "green_bike"};
    private string[] carColors = { "red_car", "blue_car", "white_car", "pink_car" };


    public int currentSecond;
     
    public int CARSPASSED = 0;
    //  public bool isTimerComplete { get; private set; } = false;
    public bool isTimerComplete = false;
    public float duration = 30f;
    public float elapsedTime;

    public float carspeed = 5f;

    
    private int[] carCounts;

    private int[] carspassed;

    public Boolean changingLights = true;
    // File paths (update these paths according to your system)
    private string jsonDataFolderPath = "/Users/shresthmishra/Documents/Code/unity_data_handling/data";
    private string pythonScriptPath = "/Users/shresthmishra/Documents/Code/other/traffic_data_to_graph.py"; // Update this path!
    private string pythonExecutable = "/Users/shresthmishra/Documents/Code/other/trafficenv/bin/python3"; // Ensure Python is in your system PATH, or provide full path

    // Start is called before the first frame update

    public List<float> timeSpent;

    public Dictionary<string, object> upStats;
    public Dictionary<string, object> downStats;
    public Dictionary<string, object> leftStats;
    public Dictionary<string, object> rightStats;

    public Dictionary<string, object> upLeftStats;
    public Dictionary<string, object> downLeftStats;
    public Dictionary<string, object> leftLeftStats;
    public Dictionary<string, object> rightLeftStats;


    // private int[] sizeCoeffArr = [0.56, 0.39, 0.24, 0.13, 0.48, 63];
    // private double[] sizeCoeffArr = {0.56, 0.39, 0.13, 0.48, 63};
    // private double[] sizeCoeffArr = {0.56, 0.56, 0.56, 0.56, 0.56, 0.56, 0.56};
    
    // private double[] sizeCoeffArr = {0.03, 0.56, 0.18, 0.39, 0.13, 0.98, 0.10};

    // private double[] sizeCoeffArr = {0.47, 0.55, 0.08, 0.53, 0.01, 0.48, 0.17};


    // private double[] sizeCoeffArr = {0.29, 0.18, 0.89, 0.31, 0.49, 0.79, 0.98};
    // private double[] sizeCoeffArr = {0.38, 0.60, 0.47, 0.24, 0.09, 0.10, 0.47};

    // private double[] sizeCoeffArr = {0.11, 0.91, 0.45, 0.79, 0.32, 0.24, 0.31};
    private double[] sizeCoeffArr = {0.58, 0.10, 0.7, 0.38, 0.53, 0.40, 0.60};

    // Optional override flag
    public bool useConfigOverride = true;
    
    // New configArr: each sub-array has 4 values for var1, var2, var3, and var4.
    // These values are chosen to be "spread out" and appear non-sequential but are predetermined.
    // private double[][] configArr = new double[][] {
    //     new double[]{ 1.0, 1.0, 1.0, 1.0 },    // config 1
    //     new double[]{ 1.5, 1.5, 1.0, 1.0 },    // config 2
    //     new double[]{ 3.0, 3.0, 1.0, 1.0 },    // config 3
    //     new double[]{ 1.5, 1.0, 1.0, 1.0 },    // config 4
    //     new double[]{ 3.0, 1.0, 1.0, 1.0 },    // config 5
    //     new double[]{ 2.5, 2.0, 1.2, 1.8 },    // config 6
    //     new double[]{ 0.8, 1.2, 0.8, 0.8 }     // config 7
    // };


    // private double[][] configArr = new double[][] {
    //     new double[]{ 2.2, 1.8, 1.1, 0.9 },    // config 8
    //     new double[]{ 1.3, 2.7, 0.9, 1.4 },    // config 9
    //     new double[]{ 2.8, 1.4, 1.5, 1.2 },    // config 10
    //     new double[]{ 1.7, 2.1, 1.3, 1.6 },    // config 11
    //     new double[]{ 3.2, 0.8, 1.7, 1.1 },    // config 12
    //     new double[]{ 2.0, 2.5, 1.0, 1.3 },    // config 13
    //     new double[]{ 0.9, 1.6, 1.2, 2.4 }     // config 14
    // };

    // private double[][] configArr = new double[][] {
    // new double[]{ 1.3, 1.0, 1.7, 0.9 },   // Config 8
    //     new double[]{ 1.5, 1.2, 0.8, 1.6 },   // Config 9
    //     new double[]{ 1.0, 1.7, 1.4, 0.85 },  // Config 10
    //     new double[]{ 1.6, 0.9, 1.2, 1.1 },   // Config 11
    //     new double[]{ 1.2, 1.5, 0.95, 1.3 },  // Config 12
    //     new double[]{ 0.9, 1.0, 1.7, 1.4 },   // Config 13
    //     new double[]{ 1.4, 1.2, 0.85, 1.6 }   // Config 14
    // };

    // private double[][] configArr = new double[][] {
    // new double[]{ 1.1, 1.4, 0.9, 1.6 },   // Config 1
    //     new double[]{ 1.3, 1.0, 1.7, 0.85 },  // Config 2
    //     new double[]{ 0.95, 1.5, 1.2, 1.4 },  // Config 3
    //     new double[]{ 1.6, 0.9, 1.3, 1.1 },   // Config 4
    //     new double[]{ 1.0, 1.7, 1.4, 0.8 },   // Config 5
    //     new double[]{ 1.5, 1.2, 1.0, 0.95 },  // Config 6
    //     new double[]{ 0.85, 1.3, 1.6, 1.1 },  // Config 7
    // };

    private double[][] configArr = new double[][] {
    new double[]{ 1.8, 1.4, 0.9, 1.1 },   // Config 8
        new double[]{ 0.8, 1.5, 1.3, 1.2 },   // Config 9
        new double[]{ 1.6, 1.0, 1.4, 0.9 },   // Config 10
        new double[]{ 1.3, 0.7, 1.5, 1.2 },   // Config 11
        new double[]{ 0.9, 1.8, 1.0, 1.4 },   // Config 12
        new double[]{ 1.2, 1.5, 0.8, 1.6 },   // Config 13
        new double[]{ 1.4, 1.1, 1.7, 0.8 }    // Config 14
    };


    




    void Start()
    {
        SetSpawnVals();
        upStats = new Dictionary<string, object> 
        {
            { "spawn coeff", upSpawn },
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };

        downStats = new Dictionary<string, object> 
        {
            { "spawn coeff", downSpawn },
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };

        leftStats = new Dictionary<string, object> 
        {
            { "spawn coeff", leftSpawn },  // Fixed to use leftSpawn
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };

        rightStats = new Dictionary<string, object> 
        {
            { "spawn coeff", rightSpawn },  // Fixed to use rightSpawn
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };
        upLeftStats = new Dictionary<string, object> 
        {
            { "spawn coeff", upLeftSpawn },
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };

        downLeftStats = new Dictionary<string, object> 
        {
            { "spawn coeff", downLeftSpawn },
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };

        leftLeftStats = new Dictionary<string, object> 
        {
            { "spawn coeff", leftLeftSpawn },  // Fixed to use leftSpawn
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };

        rightLeftStats = new Dictionary<string, object> 
        {
            { "spawn coeff", rightLeftSpawn },  // Fixed to use rightSpawn
            { "total cars generated", 0 },
            {"cars present", new int[(int)duration]},
            {"green time", 0f},
            {"is green", new bool[(int) duration]}
        };
        Application.targetFrameRate = 120;
        carCounts = new int[(int) duration];
        carspassed = new int[(int) duration];
        StartCoroutine(StartTimer(duration));
    }

    private IEnumerator StartTimer(float time)
    {
        isTimerComplete = false;
        elapsedTime = 0f; // Reset elapsed time

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time each frame
            yield return null; // Wait for next frame
        }
        isTimerComplete = true;

        ExportDataToJSON();
        // string pythonScriptPath = @"C:\Path\To\generate_graph.py";  // <<< Update this path!
        // RunPythonScript();
    }


    void ExportDataToJSON()
    {
        // Create a dictionary to hold the metrics
        Boolean optimized = gameObject.GetComponent<TrafficLightManager>().optimized;
        int fixedGreenDuration = 0;
        if(!optimized)
        {
            fixedGreenDuration = gameObject.GetComponent<TrafficLightManager>().GetGreenDuration();
        }
        Dictionary<string, object> metrics = new Dictionary<string, object>
        {
            {"optimized lights (bool)", optimized},
            {"utilizing real vals", gameObject.GetComponent<TrafficLightManager>().realVals},
            {"duration", duration},
            {"green duration (if non optimized)", fixedGreenDuration},
            {"cars passed", carspassed},
            {"congested car count", congestedCars},
            {"additional info", "N/A"}, // MAKE SURE TO ADJUST
            {"time spent", timeSpent},
            { "Up", upStats },
            { "Down", downStats },
            { "Left", leftStats },
            { "Right", rightStats },
            {"Up Turning Left", upLeftStats},
            {"Down Turning Left", downLeftStats},
            {"Left Turning Left", leftLeftStats},
            {"Right Turning Left", rightLeftStats},
            { "car counts", carCounts }
        };

        // Serialize the dictionary to JSON with indentation for readability
        string json = JsonConvert.SerializeObject(metrics, Formatting.Indented);

        // Define the folder where you want to save the JSON files.
        // Update this path to your desired folder outside of the Unity project, if needed.
        // string jsonDataFolderPath = "/Users/shresthmishra/Documents/SimulationData";

        // Ensure the export folder exists; if not, create it.
        if (!Directory.Exists(jsonDataFolderPath))
        {
            Directory.CreateDirectory(jsonDataFolderPath);
        }

        // Get all files in the folder that match the pattern "data *.json"
        string[] existingResultsFolders = Directory.GetDirectories(jsonDataFolderPath, "results*");
    

        // The new file index is one more than the count of existing files (1-indexed)
        int newIndex = existingResultsFolders.Length + 1;

        string newResultsFolderName = $"results{newIndex}";
        string newResultsFolderPath = Path.Combine(jsonDataFolderPath, newResultsFolderName);
        Directory.CreateDirectory(newResultsFolderPath);

        // Define the JSON file path inside the new results folder
        string newFilePath = Path.Combine(newResultsFolderPath, "data.json");

        // Write the JSON string to the new file
        File.WriteAllText(newFilePath, json);
        UnityEngine.Debug.Log("Exported data to JSON at: " + newFilePath);
    }


    // void RunPythonScript()
    // {
    //     ProcessStartInfo startInfo = new ProcessStartInfo
    //     {
    //         FileName = pythonExecutable,
    //         // Pass both the Python script path and the JSON file path as arguments
    //         Arguments = $"\"{pythonScriptPath}\" \"{jsonFilePath}\"",
    //         RedirectStandardOutput = true,
    //         RedirectStandardError = true,
    //         UseShellExecute = false,
    //         CreateNoWindow = true
    //     };

    //     Process process = new Process { StartInfo = startInfo };
    //     process.Start();

    //     // (Optional) Capture the output and errors for debugging
    //     string output = process.StandardOutput.ReadToEnd();
    //     string errors = process.StandardError.ReadToEnd();
    //     process.WaitForExit();

    //     UnityEngine.Debug.Log("Python script output: " + output);
    //     if (!string.IsNullOrEmpty(errors))
    //     {
    //         UnityEngine.Debug.LogError("Python script errors: " + errors);
    //     }
    // }

    public static double randomSpawnLane()
    {
        // float upperBound = 0.0045f;
        // float lowerBound = 0f;
        // double output = (double)UnityEngine.Random.Range(upperBound, lowerBound);
        // return output;
        return 0.005;
    }


    public void AddCar()
    {
        if(!isTimerComplete)
        {
            CARSPASSED += 1;
        }
    }

    private void SetSpawnVals()
    {
        upSpawn = sizeCoeff * var1;
        downSpawn = sizeCoeff * var2;
        leftSpawn = sizeCoeff * var3;  
        rightSpawn = sizeCoeff * var4;
        upLeftSpawn = sizeCoeff * var1;
        downLeftSpawn = sizeCoeff * var2;
        leftLeftSpawn = sizeCoeff * var3;
        rightLeftSpawn = sizeCoeff * var4;
        upRightSpawn = 1.5 * sizeCoeff;
        downRightSpawn = 1.5 * sizeCoeff;
        leftRightSpawn = 1.5 * sizeCoeff;
        rightRightSpawn = 1.5 * sizeCoeff;
    }
    void ChangeTraffic()
    {
        // UnityEngine.Debug.Log(sizeCoeff);
        // sizeCoeff = sizeCoeffArr[(int)(currentSecond/30)];
        // SetSpawnVals();

        UnityEngine.Debug.Log(sizeCoeff);

        // Update sizeCoeff using the predetermined sizeCoeffArr (unchanged)
        sizeCoeff = sizeCoeffArr[(int)(currentSecond / 30)];

        UnityEngine.Debug.Log(useConfigOverride);

        // Optional override: update var1, var2, var3, and var4 from configArr every 30 seconds.
        if (useConfigOverride)
        {
            
            int configIndex = (int)(currentSecond / 30);
            // Make sure we don't go out of bounds (duration is 210 so there should be exactly 7 intervals)
            if (configIndex < configArr.Length)
            {
                double[] currentConfig = configArr[configIndex];
                var1 = currentConfig[0];
                var2 = currentConfig[1];
                var3 = currentConfig[2];
                var4 = currentConfig[3];
                UnityEngine.Debug.Log($"Config override applied: var1={var1}, var2={var2}, var3={var3}, var4={var4}");
            }
        }

        // Refresh spawn values with the updated var's and sizeCoeff.
        SetSpawnVals();
    }
    void Update()
    {
        if(!isTimerComplete)
        {
            currentSecond = Mathf.FloorToInt(elapsedTime);
            

            if(changingLights && currentSecond % 30 == 0)
            {
                ChangeTraffic();
            }

            // update overall carcounts
            
            if(currentSecond > lastRecordedSecond && currentSecond < carCounts.Length)
            {
                lastRecordedSecond = currentSecond;

                int count = GameObject.FindGameObjectsWithTag("Car").Length;
                carCounts[currentSecond] = count;
                UnityEngine.Debug.Log("Second " + currentSecond + ": Car count = " + count);

                carspassed[currentSecond] = CARSPASSED;
            }






            int[] upCarsPresent = (int[]) upStats["cars present"];
            int[] downCarsPresent = (int[]) downStats["cars present"];
            int[] leftCarsPresent = (int[]) leftStats["cars present"];
            int[] rightCarsPresent = (int[]) rightStats["cars present"];
            int[] upLeftCarsPresent = (int[]) upLeftStats["cars present"];
            int[] downLeftCarsPresent = (int[]) downLeftStats["cars present"];
            int[] leftLeftCarsPresent = (int[]) leftLeftStats["cars present"];
            int[] rightLeftCarsPresent = (int[]) rightLeftStats["cars present"];
            // upCarsGenerated[(int)elapsedTime] = upCarsGenerated[(int)elapsedTime - 1] + 1;

            
            //update side by side car count
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
                if (obj.name == "car_up" && obj.transform.position.y < -3.75)
                {
                    upCars++;
                    
                }
                else if (obj.name == "car_down" && obj.transform.position.y > 3.75)
                {
                    downCars++;
                }
                else if(obj.name == "car_left" && obj.transform.position.x > 3)
                {
                    leftCars++;
                }
                else if(obj.name == "car_right" && obj.transform.position.x < -3.5)
                {
                    rightCars++;
                }
                else if (obj.name == "car_upLeft" && obj.transform.position.y < -3.75)
                {
                    upLeftCars++;
                }
                else if (obj.name == "car_downLeft" && obj.transform.position.y > 3.75)
                {
                    downLeftCars++;
                }
                else if(obj.name == "car_leftLeft" && obj.transform.position.x > 3)
                {
                    leftLeftCars++;
                }
                else if(obj.name == "car_rightLeft" && obj.transform.position.x < -3.5)
                {
                    rightLeftCars++;
                }
            }
            print(upCars);
            if(currentSecond < duration)
            {
                upCarsPresent[currentSecond] = upCars;
                downCarsPresent[currentSecond] = downCars;
                leftCarsPresent[currentSecond] = leftCars;
                rightCarsPresent[currentSecond] = rightCars;
                upLeftCarsPresent[currentSecond] = upLeftCars;
                downLeftCarsPresent[currentSecond] = downLeftCars;
                leftLeftCarsPresent[currentSecond] = leftLeftCars;
                rightLeftCarsPresent[currentSecond] = rightLeftCars;
            }
            





            

            int upCarsGenerated = (int) upStats["total cars generated"];
            int downCarsGenerated = (int) downStats["total cars generated"];
            int leftCarsGenerated = (int) leftStats["total cars generated"];
            int rightCarsGenerated = (int) rightStats["total cars generated"];

            int upLeftCarsGenerated = (int) upStats["total cars generated"];
            int downLeftCarsGenerated = (int) downStats["total cars generated"];
            int leftLeftCarsGenerated = (int) leftLeftStats["total cars generated"];
            int rightLeftCarsGenerated = (int) rightStats["total cars generated"];

            // // If we are beyond the first second, copy the previous second's count
            // if (currentSecond > lastRecordedSecond && currentSecond < carCounts.Length && currentSecond > 0)
            // {
            //     upCarsGenerated[currentSecond] = upCarsGenerated[currentSecond - 1];
            //     downCarsGenerated[currentSecond] = downCarsGenerated[currentSecond - 1];
            //     leftCarsGenerated[currentSecond] = leftCarsGenerated[currentSecond - 1];
            //     rightCarsGenerated[currentSecond] = rightCarsGenerated[currentSecond - 1];
            // }   




            frameCount += 1;
            up += randomSpawnLane() * upSpawn;
            down += randomSpawnLane() * downSpawn;
            left += randomSpawnLane() * leftSpawn;
            right += randomSpawnLane() * rightSpawn;
            upLeft += randomSpawnLane() * upLeftSpawn;
            downLeft += randomSpawnLane() * downLeftSpawn;
            leftLeft += randomSpawnLane() * leftLeftSpawn;
            rightLeft += randomSpawnLane() * rightLeftSpawn;
            upRight += randomSpawnLane() * upRightSpawn;
            downRight += randomSpawnLane() * downRightSpawn;
            leftRight += randomSpawnLane() * leftRightSpawn;
            rightRight += randomSpawnLane() * rightRightSpawn;
            upBike += randomSpawnLane() * upBikeSpawn;
            downBike += randomSpawnLane() * downBikeSpawn;
            leftBike += randomSpawnLane() * leftBikeSpawn;
            rightBike += randomSpawnLane() * rightBikeSpawn;
            upLeftBike += randomSpawnLane() * upLeftBikeSpawn;
            downLeftBike += randomSpawnLane() * downLeftBikeSpawn;
            leftLeftBike += randomSpawnLane() * leftLeftBikeSpawn;
            rightLeftBike += randomSpawnLane() * rightLeftBikeSpawn;
            upRightBike += randomSpawnLane() * upRightBikeSpawn;
            downRightBike += randomSpawnLane() * downRightBikeSpawn;
            leftRightBike += randomSpawnLane() * leftRightBikeSpawn;
            rightRightBike += randomSpawnLane() * rightRightBikeSpawn;
            if (frameCount % 120 == 0) {
                if (down >= 1 && doesSpawn()) {
                    if (SpawnCar("down", new Vector2(-2.37f, 28.87f), 270, typeof(going_down))) {
                        down -= 1;
                        downCarsGenerated ++;
                        downStats["total cars generated"] = downCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 5) {
                if (up >= 1 && doesSpawn()) {
                    if (SpawnCar("up", new Vector2(2.37f, -28.87f), 90, typeof(going_up))) {
                        up -= 1;
                        upCarsGenerated ++;
                        upStats["total cars generated"] = upCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 10) {
                if (right >= 1 && doesSpawn()) {
                    if (SpawnCar("right", new Vector2(-28.87f, -2.37f), 0, typeof(going_right))) {
                        right -= 1;
                        rightCarsGenerated ++;
                        rightStats["total cars generated"] = rightCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 15) {
                if (left >= 1 && doesSpawn()) {
                    if (SpawnCar("left", new Vector2(28.87f, 1.9f), 180, typeof(going_left))) {
                        left -= 1;
                        leftCarsGenerated ++;
                        leftStats["total cars generated"] = leftCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 20) {
                if (upLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("upLeft", new Vector2(0.8f, -28.87f), 90, typeof(going_up_left))) {
                        upLeft -= 1;
                        upLeftCarsGenerated ++;
                        upLeftStats["total cars generated"] = upLeftCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 0) {
                if (downLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("downLeft", new Vector2(-0.8f, 28.87f), 270, typeof(going_down_left))) {
                        downLeft -= 1;
                        downLeftCarsGenerated ++;
                        downLeftStats["total cars generated"] = downLeftCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 30) {
                if (leftLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("leftLeft", new Vector2(28.87f, 0.8f), 180, typeof(going_left_left))) {
                        leftLeft -= 1;
                        leftLeftCarsGenerated ++;
                        leftLeftStats["total cars generated"] = downLeftCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 35) {
                if (rightLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("rightLeft", new Vector2(-28.87f, -0.8f), 0, typeof(going_right_left))) {
                        rightLeft -= 1;
                        rightLeftCarsGenerated ++;
                        rightLeftStats["total cars generated"] = rightLeftCarsGenerated;
                    }
                }
            }
            if (frameCount % 120 == 40) {
                if (upRight >= 1 && doesSpawn()) {
                    if (SpawnCar("upRight", new Vector2(3.94f, -28.87f), 90, typeof(going_up_right))) {
                        upRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 45) {
                if (downRight >= 1 && doesSpawn()) {
                    if (SpawnCar("downRight", new Vector2(-3.94f, 28.87f), 270, typeof(going_down_right))) {
                        downRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 50) {
                if (leftRight >= 1 && doesSpawn()) {
                    if (SpawnCar("leftRight", new Vector2(28.87f, 3.94f), 180, typeof(going_left_right))) {
                        leftRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 55) {
                if (rightRight >= 1 && doesSpawn()) {
                    if (SpawnCar("rightRight", new Vector2(-28.87f, -3.94f), 0, typeof(going_right_right))) {
                        rightRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 60) {
                if (downBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("downBike", new Vector2(-2.37f, 21.3f), 270, typeof(going_down))) {
                        downBike -= 1;
                    }
                }
                if (upBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("upBike", new Vector2(2.37f, -21.3f), 90, typeof(going_up))) {
                        upBike -= 1;
                    }
                }
                if (rightBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("rightBike", new Vector2(-21.3f, -2.37f), 360, typeof(going_right))) {
                        rightBike -= 1;
                    }
                }
                if (leftBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("leftBike", new Vector2(21.3f, 2.4f), 180, typeof(going_left))) {
                        leftBike -= 1;
                    }
                }

                if (upLeftBike >= 1 && doesSpawn())
                {       
                    if (SpawnBike("upLeftBike", new Vector2(0.8f, -21.5f), 90, typeof(going_up_left))) {
                        upLeftBike -= 1;
                    }
                }
                if (downLeftBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("downLeftBike", new Vector2(-0.8f, 21.5f), 270, typeof(going_down_left))) {
                        downLeftBike -= 1;
                    }
                }
                if (leftLeftBike >= 1 && doesSpawn())
                { 
                    if (SpawnBike("leftLeftBike", new Vector2(21.5f, 0.8f), 180, typeof(going_left_left))) {
                        leftLeftBike -= 1;
                    }
                }
                if (rightLeftBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("rightLeftBike", new Vector2(-21.5f, -0.8f), 360, typeof(going_right_left))) {
                        rightLeftBike -= 1;
                    }
                }
                if (upRightBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("upRightBike", new Vector2(3.94f, -21.5f), 90, typeof(going_up_right))) {
                        upRightBike -= 1;
                    }
                }
                if (downRightBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("downRightBike", new Vector2(-3.94f, 21.5f), 270, typeof(going_down_right))) {
                        downRightBike -= 1;
                    }
                }
                if (leftRightBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("leftRightBike", new Vector2(21.5f, 3.94f), 180, typeof(going_left_right))) {
                        leftRightBike -= 1;
                    }
                }
                if (rightRightBike >= 1 && doesSpawn())
                {
                    if (SpawnBike("rightRightBike", new Vector2(-21.5f, -3.94f), 360, typeof(going_right_right))) {
                        rightRightBike -= 1;
                    }
                }
            }
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     UnityEngine.Debug.Log("A key was pressed");
            // }
        }
    }

    bool SpawnCar(string name, Vector2 position, float rotation, System.Type componentType)
    {
        bool isOpen = Physics2D.OverlapPoint(position) == null;
        float radians = rotation * Mathf.Deg2Rad; // convert to radian
        Vector2 unitVector = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        

        if(!isOpen)
        {
            congestedCars++;
        }

        for(float i = -3.5f; i<3.5f; i += 0.1f)
        {
            if(isOpen)
            {
                isOpen = Physics2D.OverlapPoint(position + i * unitVector) == null;
            }
            else
            {
                break;
            }
        }
        
        while(!isOpen)
        {
            // position += -7 * new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // for(float i = -3.5f; i<3.5f; i += 0.1f)
            // {
            //     isOpen = Physics2D.OverlapPoint(position + i * unitVector) == null;
            // }

            // position += -3 * new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            // isOpen = Physics2D.OverlapPoint(position) == null;
            bool openSpot = true;
            for(float i = -5f; i<5f; i += 0.1f)
            {
                if(Physics2D.OverlapPoint(position + i * unitVector) == null)
                {
                    continue;
                }
                else
                {
                    openSpot = false;
                    break;
                }
            }
            isOpen = openSpot;
            position += -3 * new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }
        if (isOpen)
        {
            if(Vector2.Distance(new Vector2(0, 0), position) > gameObject.GetComponent<TrafficLightManager>().GetGreenDuration() * 3)
            {
                return true;
            }
            bool isAmbulance = UnityEngine.Random.Range(0, 100) < -1;
            if(isAmbulance)
            {
                GameObject ambulance = new GameObject($"ambulance_{name}");
                ambulance.transform.position = position;
                ambulance.transform.rotation = Quaternion.Euler(0, 0, rotation);

                SpriteRenderer spriteRenderer = ambulance.AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 1;
                spriteRenderer.sprite = Resources.Load<Sprite>("ambulance");
                ambulance.transform.localScale = new Vector2(0.7f, 0.7f);
                ambulance.AddComponent(componentType);
                // ambulance.transform.localScale = new Vector2(0.1f. 0.1f);

            }
            else
            {
                GameObject car = new GameObject($"car_{name}");
                car.transform.position = position;
                car.transform.rotation = Quaternion.Euler(0, 0, rotation);

                SpriteRenderer spriteRenderer = car.AddComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 1;
                int typeOfCar = UnityEngine.Random.Range(0, carColors.Length);
                spriteRenderer.sprite = Resources.Load<Sprite>(carColors[typeOfCar]);
                // if (typeOfCar == 0) {
                //     car.transform.localScale = new Vector2(0.07f, 0.07f);
                // }
                // else if (typeOfCar == 1) {
                //     car.transform.localScale = new Vector2(0.24f, 0.24f);
                // }
                // else if (typeOfCar == 2 || typeOfCar == 3) {
                //     car.transform.localScale = new Vector2(0.31f, 0.31f);
                // }
                car.transform.localScale = new Vector2(0.3f, 0.3f);
                car.AddComponent(componentType);
            }
            
            return true;
        }
        return false;
    }
    bool SpawnBike(string name, Vector2 position, float rotation, System.Type componentType)
    {
        bool isOpen = Physics2D.OverlapPoint(position) == null;
        if (isOpen) {
            GameObject bike = new GameObject($"bike_{name}");
            bike.transform.position = position;
            bike.transform.rotation = Quaternion.Euler(0, 0, rotation);
            
            SpriteRenderer spriteRenderer = bike.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 1;
            int typeOfBike = UnityEngine.Random.Range(0, bikeColors.Length);
            spriteRenderer.sprite = Resources.Load<Sprite>(bikeColors[typeOfBike]);
            bike.AddComponent(componentType);

            if (typeOfBike == 0 || typeOfBike == 1) {
                bike.transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else if (typeOfBike == 3) {
                bike.transform.localScale = new Vector2(0.35f, 0.35f);
            }
            return true;
        }
        return false;
    }

    bool doesSpawn() {
        // int doesSpawn = Random.Range(0, 3);
        // if (doesSpawn == 1) {
        //     return false;
        // }
        // else {
        //     return true;
        // }
        return true;
    }

}
