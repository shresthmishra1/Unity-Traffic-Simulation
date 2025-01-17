using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class going_left_left : MonoBehaviour
{
    float speed = 10f;
    bool startedCollision = false;

    public Transform[] waypoints; // Array of waypoints for the path
    public int currentWaypointIndex = 0; // Tracks the current waypoint

    void Start()
    {
        // Automatically find all GameObjects with the tag "Waypoint"
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("LeftLeftWaypoint");

        // Sort waypoints by name or position if needed
        System.Array.Sort(waypointObjects, (a, b) => string.Compare(a.name, b.name));

        // Assign their transforms to the waypoints array
        waypoints = new Transform[waypointObjects.Length];
        for (int i = 0; i < waypointObjects.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform;
            //Debug.Log(waypoints[i].position);
        }
        

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        float speed = UnityEngine.Random.Range(2.5f, 12.5f);
        startCar();
        gameObject.AddComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();

        if(-21 >= transform.position.x)
        {
            Destroy(gameObject);
        }
        if(inStopRange()) 
        {
            bool isRed = sortedLightList[3].isRed;
            // Debug.Log("inStopRange true block");
            if(!isRed) 
            {
                // Debug.Log("greenlight block");
                startCar();
            }
            else if(isRed)
            {
                // Debug.Log("redlight block");
                stopCar();
            }
            else
            {
                // Debug.Log("should not be coming here");
            }
        } 
        else
        {
            // Debug.Log("DEFINETLY should not be coming here");
        }

        // Move towards the current waypoint
        if (currentWaypointIndex < waypoints.Length)
        {
            var direction = FindDirection();
            // Move the car towards the waypoint
            rb.linearVelocity = direction * speed;
            RotateSprite(direction);

            // Check if the car is close to the waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
            {
                currentWaypointIndex++;
            }
        }
    }


    public Vector3 FindDirection() {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - this.transform.position).normalized;
        return direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D thisrb = GetComponent<Rigidbody2D>();
        Rigidbody2D otherRb = collision.rigidbody;

        if (thisrb.position.y > otherRb.position.y)
        {
            startedCollision = true;
            stopCar();
        }
        else
        {
            startedCollision = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.position.y < this.transform.position.y)
        {
            stopCar();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.position.y < this.transform.position.y)
        {
            startedCollision = false;
            startCar();
        }
    }
    
    private bool inStopRange()
    {

        return 6f <= transform.position.x && transform.position.x <= 7.7f;
    }

    private void startCar()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(0, -speed);
    }

    private void stopCar()
    {
        //Debug.Log("stopped");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void RotateSprite(Vector3 direction)
    {
        // Calculate the angle based on the direction vector
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        // Rotate the GameObject to match the angle
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
