using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    double upSpawn = 12;
    double downSpawn = 12;
    double leftSpawn = 96;
    double rightSpawn = 36;
    double upLeftSpawn = 0;
    double downLeftSpawn = 0;
    double leftLeftSpawn = 0;
    double rightLeftSpawn = 0;
    double upRightSpawn = 0.75;
    double downRightSpawn = 0.75;
    double leftRightSpawn = 0.75;
    double rightRightSpawn = 0.75;
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


    

    private string[] bikeColors = { "orange_bike", "green_bike"};
    private string[] carColors = { "red_car", "blue_car", "white_car", "pink_car" };

     
     public int CARSPASSED = 0;
    //  public bool isTimerComplete { get; private set; } = false;
    public bool isTimerComplete = false;
    public float duration = 30f;
    public float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
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
    }
    public static double randomSpawnLane()
    {
        // float upperBound = 0.0045f;
        // float lowerBound = 0f;
        // double output = (double)UnityEngine.Random.Range(upperBound, lowerBound);
        // return output;
        return 0.05;
    }


    public void AddCar()
    {
        if(!isTimerComplete)
        {
            CARSPASSED += 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isTimerComplete)
        {
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
                    if (SpawnCar("down", new Vector2(-2.37f, 21.3f), 270, typeof(going_down))) {
                        down -= 1;
                    }
                }
            }
            if (frameCount % 120 == 5) {
                if (up >= 1 && doesSpawn()) {
                    if (SpawnCar("up", new Vector2(2.37f, -21.3f), 90, typeof(going_up))) {
                        up -= 1;
                    }
                }
            }
            if (frameCount % 120 == 10) {
                if (right >= 1 && doesSpawn()) {
                    if (SpawnCar("right", new Vector2(-21.3f, -2.37f), 0, typeof(going_right))) {
                        right -= 1;
                    }
                }
            }
            if (frameCount % 120 == 15) {
                if (left >= 1 && doesSpawn()) {
                    if (SpawnCar("left", new Vector2(21.3f, 2.4f), 180, typeof(going_left))) {
                        left -= 1;
                    }
                }
            }
            if (frameCount % 120 == 20) {
                if (upLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("upLeft", new Vector2(0.8f, -21.5f), 90, typeof(going_up_left))) {
                        upLeft -= 1;
                    }
                }
            }
            if (frameCount % 120 == 25) {
                if (downLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("downLeft", new Vector2(-0.8f, 21.5f), 270, typeof(going_down_left))) {
                        downLeft -= 1;
                    }
                }
            }
            if (frameCount % 120 == 30) {
                if (leftLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("leftLeft", new Vector2(21.5f, 0.8f), 180, typeof(going_left_left))) {
                        leftLeft -= 1;
                    }
                }
            }
            if (frameCount % 120 == 35) {
                if (rightLeft >= 1 && doesSpawn()) {
                    if (SpawnCar("rightLeft", new Vector2(-21.5f, -0.8f), 0, typeof(going_right_left))) {
                        rightLeft -= 1;
                    }
                }
            }
            if (frameCount % 120 == 40) {
                if (upRight >= 1 && doesSpawn()) {
                    if (SpawnCar("upRight", new Vector2(3.94f, -21.5f), 90, typeof(going_up_right))) {
                        upRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 45) {
                if (downRight >= 1 && doesSpawn()) {
                    if (SpawnCar("downRight", new Vector2(-3.94f, 21.5f), 270, typeof(going_down_right))) {
                        downRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 50) {
                if (leftRight >= 1 && doesSpawn()) {
                    if (SpawnCar("leftRight", new Vector2(21.5f, 3.94f), 180, typeof(going_left_right))) {
                        leftRight -= 1;
                    }
                }
            }
            if (frameCount % 120 == 55) {
                if (rightRight >= 1 && doesSpawn()) {
                    if (SpawnCar("rightRight", new Vector2(-21.5f, -3.94f), 0, typeof(going_right_right))) {
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A key was pressed");
            }
        }
    }

    bool SpawnCar(string name, Vector2 position, float rotation, System.Type componentType)
    {
        bool isOpen = Physics2D.OverlapPoint(position) == null;
        if (isOpen)
        {
            GameObject car = new GameObject($"car_{name}");
            car.transform.position = position;
            car.transform.rotation = Quaternion.Euler(0, 0, rotation);

            SpriteRenderer spriteRenderer = car.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 1;
            int typeOfCar = Random.Range(0, carColors.Length);
            spriteRenderer.sprite = Resources.Load<Sprite>(carColors[typeOfCar]);
            if (typeOfCar == 0) {
                car.transform.localScale = new Vector2(0.1f, 0.1f);
            }
            else if (typeOfCar == 1) {
                car.transform.localScale = new Vector2(0.28f, 0.28f);
            }
            else if (typeOfCar == 2 || typeOfCar == 3) {
                car.transform.localScale = new Vector2(0.35f, 0.35f);
            }
            car.AddComponent(componentType);
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
            int typeOfBike = Random.Range(0, bikeColors.Length);
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
        int doesSpawn = Random.Range(0, 3);
        if (doesSpawn == 1) {
            return false;
        }
        else {
            return true;
        }
    }

}
