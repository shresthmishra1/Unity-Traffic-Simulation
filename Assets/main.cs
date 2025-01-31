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
    double upSpawn = 1;
    double downSpawn = 1;
    double leftSpawn = 1;
    double rightSpawn = 1;
    double upLeftSpawn = 0.4;
    double downLeftSpawn = 0.4;
    double leftLeftSpawn = 0.4;
    double rightLeftSpawn = 0.4;
    double upRightSpawn = 0.4;
    double downRightSpawn = 0.4;
    double leftRightSpawn = 0.4;
    double rightRightSpawn = 0.4;
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

    double upBikeSpawn = 0.25;
    double downBikeSpawn = 0.25;
    double leftBikeSpawn = 0.25;
    double rightBikeSpawn = 0.25;
    double upLeftBikeSpawn = 0.1;
    double downLeftBikeSpawn = 0.1;
    double leftLeftBikeSpawn = 0.1;
    double rightLeftBikeSpawn = 0.1;
    double upRightBikeSpawn = 0.1;
    double downRightBikeSpawn = 0.1;
    double leftRightBikeSpawn = 0.1;
    double rightRightBikeSpawn = 0.1;

    private string[] bikeColors = { "red_bike", "orange_bike", "green_bike"};
    private string[] carColors = { "red_car", "blue_car", "white_car", "pink_car" };

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    public static double randomSpawnLane()
    {
        float upperBound = 0.0045f;
        float lowerBound = 0f;
        double output = (double)UnityEngine.Random.Range(upperBound, lowerBound);
        return output;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        if (down >= 1)
        {
            down -= 1;
            SpawnCar("down", new Vector2(-2.37f, 21.3f), 270, typeof(going_down));
        }
        if (up >= 1)
        {
            up -= 1;
            SpawnCar("up", new Vector2(2.37f, -21.3f), 90, typeof(going_up));
        }
        if (right >= 1)
        {
            right -= 1;
            SpawnCar("right", new Vector2(-21.3f, -2.37f), 0, typeof(going_right));
        }
        if (left >= 1)
        {
            left -= 1;
            SpawnCar("left", new Vector2(21.3f, 2.4f), 180, typeof(going_left));
        }
        if (upLeft >= 1)
        {
            upLeft -= 1;
            SpawnCar("upLeft", new Vector2(0.8f, -21.5f), 90, typeof(going_up_left));
        }
        if (downLeft >= 1)
        {
            downLeft -= 1;
            SpawnCar("downLeft", new Vector2(-0.8f, 21.5f), 270, typeof(going_down_left));
        }
        if (leftLeft >= 1)
        {
            leftLeft -= 1;
            SpawnCar("leftLeft", new Vector2(21.5f, 0.8f), 180, typeof(going_left_left));
        }
        if (rightLeft >= 1)
        {
            rightLeft -= 1;
            SpawnCar("rightLeft", new Vector2(-21.5f, -0.8f), 0, typeof(going_right_left));
        }
        if (upRight >= 1)
        {
            upRight -= 1;
            SpawnCar("upRight", new Vector2(3.94f, -21.5f), 90, typeof(going_up_right));
        }
        if (downRight >= 1)
        {
            downRight -= 1;
            SpawnCar("downRight", new Vector2(-3.94f, 21.5f), 270, typeof(going_down_right));
        }
        if (leftRight >= 1)
        {
            leftRight -= 1;
            SpawnCar("leftRight", new Vector2(21.5f, 3.94f), 180, typeof(going_left_right));
        }
        if (rightRight >= 1)
        {
            rightRight -= 1;
            SpawnCar("rightRight", new Vector2(-21.5f, -3.94f), 0, typeof(going_right_right));
        }

        if (downBike >= 1)
        {
            downBike -= 1;
            SpawnBike("downBike", new Vector2(-2.37f, 21.3f), 270, typeof(going_down));
        }
        if (upBike >= 1)
        {
            upBike -= 1;
            SpawnBike("upBike", new Vector2(2.37f, -21.3f), 90, typeof(going_up));
        }
        if (rightBike >= 1)
        {
            rightBike -= 1;
            SpawnBike("rightBike", new Vector2(-21.3f, -2.37f), 360, typeof(going_right));
        }
        if (leftBike >= 1)
        {
            leftBike -= 1;
            SpawnBike("leftBike", new Vector2(21.3f, 2.4f), 180, typeof(going_left));
        }

        if (upLeftBike >= 1)
        {
            upLeftBike -= 1;
            SpawnBike("upLeftBike", new Vector2(0.8f, -21.5f), 90, typeof(going_up_left));
        }
        if (downLeftBike >= 1)
        {
            downLeftBike -= 1;
            SpawnBike("downLeftBike", new Vector2(-0.8f, 21.5f), 270, typeof(going_down_left));
        }
        if (leftLeftBike >= 1)
        {
            leftLeftBike -= 1;
            SpawnBike("leftLeftBike", new Vector2(21.5f, 0.8f), 180, typeof(going_left_left));
        }
        if (rightLeftBike >= 1)
        {
            rightLeftBike -= 1;
            SpawnBike("rightLeftBike", new Vector2(-21.5f, -0.8f), 360, typeof(going_right_left));
        }
        if (upRightBike >= 1)
        {
            upRightBike -= 1;
            SpawnBike("upRightBike", new Vector2(3.94f, -21.5f), 90, typeof(going_up_right));
        }
        if (downRightBike >= 1)
        {
            downRightBike -= 1;
            SpawnBike("downRightBike", new Vector2(-3.94f, 21.5f), 270, typeof(going_down_right));
        }
        if (leftRightBike >= 1)
        {
            leftRightBike -= 1;
            SpawnBike("leftRightBike", new Vector2(21.5f, 3.94f), 180, typeof(going_left_right));
        }
        if (rightRightBike >= 1)
        {
            rightRightBike -= 1;
            SpawnBike("rightRightBike", new Vector2(-21.5f, -3.94f), 360, typeof(going_right_right));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A key was pressed");
        }
    }

    void SpawnCar(string name, Vector2 position, float rotation, System.Type componentType)
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
    }
    void SpawnBike(string name, Vector2 position, float rotation, System.Type componentType)
{
    GameObject bike = new GameObject($"bike_{name}");
    bike.transform.position = position;
    bike.transform.rotation = Quaternion.Euler(0, 0, rotation);
    
    SpriteRenderer spriteRenderer = bike.AddComponent<SpriteRenderer>();
    spriteRenderer.sortingOrder = 1;
    int typeOfBike = Random.Range(0, bikeColors.Length);
    spriteRenderer.sprite = Resources.Load<Sprite>(bikeColors[typeOfBike]);
    bike.AddComponent(componentType);

    if (typeOfBike == 0) {
        bike.transform.localScale = new Vector2(1f, 1f);
    }
    else if (typeOfBike == 1 || typeOfBike == 2) {
        bike.transform.localScale = new Vector2(0.5f, 0.5f);
    }
    else if (typeOfBike == 3) {
        bike.transform.localScale = new Vector2(0.35f, 0.35f);
    }

}

}
