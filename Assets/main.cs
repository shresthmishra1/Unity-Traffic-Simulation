using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class main : MonoBehaviour
{
	double top = 0;
    double bottom = 0;
    double left = 0;
    double right = 0;
    double topSpawn = 1;
    double bottomSpawn = 1;
    double leftSpawn = 1;
    double rightSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }


    public static double randomSpawnLane() {
        float topBound = 0.0045f;
        float bottomBound = 0f;
        double output = (double)UnityEngine.Random.Range(bottomBound, topBound);
        Debug.Log(output);
        return output;
    }


    // Update is called once per frame
    void Update()
    {
        /*
        double topLeftSpawn = 0.25;
        double bottomLeftSpawn = 0.25;
        double leftLeftSpawn = 0.25;
        double rightLeftSpawn = 0.25;
        */
        top += randomSpawnLane() * topSpawn;
        bottom += randomSpawnLane() * bottomSpawn;
        left += randomSpawnLane() * leftSpawn;
        right += randomSpawnLane() * rightSpawn;
        /*
        trafficData.topLeft += randomSpawnLane() * topLeftSpawn;
        trafficData.bottomLeft += randomSpawnLane() * bottomLeftSpawn;
        trafficData.leftLeft += randomSpawnLane() * leftLeftSpawn;
        trafficData.rightLeft += randomSpawnLane() * rightLeftSpawn;
        */
        if (top >= 1) {
            top -= 1;
            GameObject car_down = new GameObject("cool_car_down");
            car_down.transform.position = new Vector2(-2.37f, 21.3f);
            SpriteRenderer spriteRenderer = car_down.AddComponent<SpriteRenderer>();
            car_down.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_down");
            car_down.AddComponent<going_down>();
        }
        if (bottom >= 1) {
            bottom -= 1;
            GameObject car_up = new GameObject("cool_car_up");
            car_up.transform.position = new Vector2(2.37f, -21.3f);
            SpriteRenderer spriteRenderer = car_up.AddComponent<SpriteRenderer>();
            car_up.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_up");
            car_up.AddComponent<going_up>();
        }
        if (left >= 1) {
            left -= 1;
            GameObject car_right = new GameObject("cool_car_right");
            car_right.transform.position = new Vector2(-21.3f, -2.37f);
            SpriteRenderer spriteRenderer = car_right.AddComponent<SpriteRenderer>();
            car_right.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_right");
            car_right.AddComponent<going_right>();
        }
        if (right >= 1) {
            right -= 1;
            GameObject car_left = new GameObject("cool_car_left");
            car_left.transform.position = new Vector2(21.3f, 2.4f);
            SpriteRenderer spriteRenderer = car_left.AddComponent<SpriteRenderer>();
            car_left.transform.localScale = new Vector2(0.1f, 0.1f);
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.sprite = Resources.Load<Sprite>("cool_car_left");
            car_left.AddComponent<going_left>();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("space key was pressed");
        }
    }
}
