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
    double upSpawn = 0;
    double downSpawn = 0;
    double leftSpawn = 0;
    double rightSpawn = 0;
    double upLeftSpawn = 0;
    double downLeftSpawn = 0;
    double leftLeftSpawn = 0;
    double rightLeftSpawn = 0;
    double upRightSpawn = 1;
    double downRightSpawn = 1;
    double leftRightSpawn = 1;
    double rightRightSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }


    public static double randomSpawnLane() {
        float upperBound = 0.0045f;
        float lowerBound = 0f;
        double output = (double)UnityEngine.Random.Range(upperBound, lowerBound);
        // Debug.Log(output);
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
        // Debug.Log(rightLeft);

        if (down >= 1) {
            down -= 1;
            GameObject car_down = new GameObject("cool_car_down");
            car_down.transform.position = new Vector2(-2.37f, 21.3f);
            SpriteRenderer spriteRenderer = car_down.AddComponent<SpriteRenderer>();
            car_down.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_down");
            car_down.AddComponent<going_down>();
        }
        if (up >= 1) {
            up -= 1;
            GameObject car_up = new GameObject("cool_car_up");
            car_up.transform.position = new Vector2(2.37f, -21.3f);
            SpriteRenderer spriteRenderer = car_up.AddComponent<SpriteRenderer>();
            car_up.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_up");
            car_up.AddComponent<going_up>();
        }
        if (right >= 1) {
            right -= 1;
            GameObject car_right = new GameObject("cool_car_right");
            car_right.transform.position = new Vector2(-21.3f, -2.37f);
            SpriteRenderer spriteRenderer = car_right.AddComponent<SpriteRenderer>();
            car_right.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_right");
            car_right.AddComponent<going_right>();
        }
        if (left >= 1) {
            left -= 1;
            GameObject car_left = new GameObject("cool_car_left");
            car_left.transform.position = new Vector2(21.3f, 2.4f);
            SpriteRenderer spriteRenderer = car_left.AddComponent<SpriteRenderer>();
            car_left.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_left");
            car_left.AddComponent<going_left>();
        }
        if (upLeft >= 1) {
            upLeft -= 1;
            GameObject car_up = new GameObject("cool_car_up");
            car_up.transform.position = new Vector2(0.8f, -21.5f); 
            SpriteRenderer spriteRenderer = car_up.AddComponent<SpriteRenderer>();
            car_up.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_up");
            car_up.AddComponent<going_up_left>();
        }
        if (downLeft >= 1) {
            downLeft -= 1;
            GameObject car_down = new GameObject("cool_car_down");
            car_down.transform.position = new Vector2(-0.8f, 21.5f); 
            SpriteRenderer spriteRenderer = car_down.AddComponent<SpriteRenderer>();
            car_down.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_down");
            car_down.AddComponent<going_down_left>();
        }
        if (rightLeft >= 1) {
            rightLeft -= 1;
            GameObject car_right = new GameObject("cool_car_right");
            car_right.transform.position = new Vector2(-21.5f, -0.8f); 
            SpriteRenderer spriteRenderer = car_right.AddComponent<SpriteRenderer>();
            car_right.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_right");
            car_right.AddComponent<going_right_left>();
        }
        if (leftLeft >= 1) {
            leftLeft -= 1;
            GameObject car_left = new GameObject("cool_car_left");
            car_left.transform.position = new Vector2(21.5f, 0.8f); 
            SpriteRenderer spriteRenderer = car_left.AddComponent<SpriteRenderer>();
            car_left.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_left");
            car_left.AddComponent<going_left_left>();
        }
         if (upRight >= 1) {
            upRight -= 1;
            GameObject car_up = new GameObject("cool_car_up");
            car_up.transform.position = new Vector2(3.94f, -21.5f); 
            SpriteRenderer spriteRenderer = car_up.AddComponent<SpriteRenderer>();
            car_up.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_up");
            car_up.AddComponent<going_up_right>();
        }
        if (downRight >= 1) {
            downRight -= 1;
            GameObject car_down = new GameObject("cool_car_down");
            car_down.transform.position = new Vector2(-3.94f, 21.5f); 
            SpriteRenderer spriteRenderer = car_down.AddComponent<SpriteRenderer>();
            car_down.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_down");
            car_down.AddComponent<going_down_right>();
        }
        if (rightRight >= 1) {
            rightRight -= 1;
            GameObject car_right = new GameObject("cool_car_right");
            car_right.transform.position = new Vector2(-21.5f, -3.94f); 
            SpriteRenderer spriteRenderer = car_right.AddComponent<SpriteRenderer>();
            car_right.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_right");
            car_right.AddComponent<going_right_right>();
        }
        if (leftRight >= 1) {
            leftRight -= 1;
            GameObject car_left = new GameObject("cool_car_left");
            car_left.transform.position = new Vector2(21.5f, 3.94f); 
            SpriteRenderer spriteRenderer = car_left.AddComponent<SpriteRenderer>();
            car_left.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_left");
            car_left.AddComponent<going_left_right>();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("space key was pressed");
        }
    }
}
