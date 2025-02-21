using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Mathematics;
public class going_down_left : MonoBehaviour
{
    float speed = 10f;
    bool startedCollision = false;
    bool stopped = false;

    public Transform[] waypoints; // Array of waypoints for the path
    public int currentWaypointIndex = 0; // Tracks the current waypoint

    public float carStopDistance = 1f; // Minimum distance to stop the car a little before.
    public float decelerationRate = 13f; // Rate to slow down smoothly.
    public float acelerationRate = 1f; 
    Vector2 size;
    Vector3 scale;
    Vector3 offset;
    public float startTime;
    void Start()
    {
        // Automatically find all GameObjects with the tag "Waypoint"
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("DownLeftWaypoint");

        // Sort waypoints by name or position if needed
        System.Array.Sort(waypointObjects, (a, b) => string.Compare(a.name, b.name));

        // Assign their transforms to the waypoints array
        waypoints = new Transform[waypointObjects.Length];
        for (int i = 0; i < waypointObjects.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform;
        }
        gameObject.tag = "Car";
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        // speed = UnityEngine.Random.Range(7.5f, 12f);
        graduallyStartCar();
        gameObject.AddComponent<BoxCollider2D>();
        BoxCollider2D boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        size = boxCollider.size;
        scale = this.transform.localScale;
        // Debug.Log(size);
       // offset = size.x * 0.5f * scale.x + 0.6f;
       startTime = Time.time;
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();
        
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            if(currentWaypointIndex == waypoints.Length -1)
            {
                GameObject mainObj = GameObject.FindGameObjectWithTag("MainCamera");
                main mainscript = mainObj.GetComponent<main>();
                mainscript.AddCar();
                float endTime = Time.time;
                mainscript.timeSpent.Add(endTime - startTime);
                Destroy(gameObject);
            }
            else
            {
                currentWaypointIndex++;
            }
        }
        // graduallyStartCar();

        var direction = FindDirection().normalized;
        offset = direction * size.y * scale.y * 0.6f + 0.6f * direction;
        int layerNum = 0;
        string layerName = LayerMask.LayerToName(layerNum);
        int layerMask = LayerMask.GetMask(layerName);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, direction, carStopDistance);
        if (hit.collider != null) 
        {
            //Debug.Log("raycast hitting something");
            bool isRed = sortedLightList[1].lightPhase == 2;
            bool isYellow = sortedLightList[1].lightPhase == 1;
        // Debug.Log("inStopRange true block");
        
            // Debug.Log(hit.collider.gameObject.tag + " THIS IS WHAT THE CAR IS DETECTING");
            // Debug.Log(hit.transform);
            float distanceToOther = hit.distance;
            if (hit.collider.gameObject.tag == "Car")
            {
                //Debug.Log(hit.collider.gameObject.tag);
                // Debug.Log("jIOWEFJIOWFJIEWFJEWIOFJWEIOFJEWIOFJWEIOJFWEIOJFEIO");
                

                if (distanceToOther <= carStopDistance)
                {
                    // Debug.Log("THE CAR WILL NOW BE STOPPED AHAHAHAHAHAHAHAH");
                    stopCar();
                }
                else
                {
                    // startCar();
                    graduallyStartCar();
                }
            }
            


            else if (hit.collider.gameObject.tag == "Finish")
            {
                if(distanceToOther <= carStopDistance && (isRed || isYellow))
                {
                    // Debug.Log("THE CAR WILL NOW BE STOPPED AHAHAHAHAHAHAHAH");
                    stopCar();
                }
                else
                {
                    // startCar();
                    graduallyStartCar();
                }
            }
        }
        else
        {
            graduallyStartCar();
        }
    }


    public Vector3 FindDirection() {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 direction = (targetPosition - this.transform.position).normalized;
        return direction;
    }

    

    private void graduallyStartCar()
    {
        

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.linearVelocity = new Vector2(speed, 0);
        // stopped = false;
        
        var direction = FindDirection();
        // Move the car towards the waypoint
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, speed * direction, decelerationRate * Time.fixedDeltaTime);
        // rb.linearVelocity = speed*direction;
        RotateSprite(direction);

        // Check if the car is close to the waypoint

        
    }

    private void stopCar()
    {
        //Debug.Log("car will stop");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, decelerationRate * Time.fixedDeltaTime); //Stop car from moving
        rb.angularVelocity = 0f;
        stopped = true;
    }

    private void RotateSprite(Vector3 direction)
    {
        float angle = Mathf.Atan2(math.abs(direction.y), math.abs(direction.x)) * Mathf.Rad2Deg;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);
        // transform.rotation = targetRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
    }

    // void OnDrawGizmos()
    // {
    //     // Visualize the ray in the Scene view.
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(transform.transform.position + offset, FindDirection() * carStopDistance);
    // }
}
