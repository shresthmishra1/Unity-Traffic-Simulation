using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Callbacks;
using System.Linq;

public class going_left : MonoBehaviour
{
    static GameObject mainObj;
    static main mainscript;
    float speed;
    //[SerializeField] right_traffic_light trafficlight;
    bool startedCollision = false;
    // public float detectionDistance = 0.5f; // Distance to detect the car in front.
    public float carStopDistance = 1f; // Minimum distance to stop the car a little before.
    public float decelerationRate = 10f; // Rate to slow down smoothly.
    public float acelerationRate = 10f;
    float offset;

    public float startTime;
    void Start()
    {
        mainObj = GameObject.FindGameObjectWithTag("MainCamera");
        mainscript = mainObj.GetComponent<main>();
        speed = mainscript.carspeed;
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        // float speed = UnityEngine.Random.Range(2.5f, 12.5f);
        // startCar();
        graduallyStartCar();
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.tag = "Car";
        BoxCollider2D boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        Vector2 size = boxCollider.size;
        Vector3 scale = this.transform.localScale;
        // Debug.Log(size);
        offset = size.x*0.5f*scale.x+0.4f;
        // offset = size.x*0.5f;
        startTime = Time.time;
    }

    

    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var Lightlist = new List<traffic_light>(FindObjectsOfType<traffic_light>());
        var sortedLightList = Lightlist.OrderBy(traffic_light => traffic_light.light_number).ToList();

        // Step 1: resets car position if car goes off screen
        if(-29 >= transform.position.x) 
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
            int layerNum = 0;
            string layerName = LayerMask.LayerToName(layerNum);
            int layerMask = LayerMask.GetMask(layerName);
            RaycastHit2D hit = Physics2D.Raycast(transform.transform.position + new Vector3(-offset,0f,0f), Vector3.left, carStopDistance);
            if(hit.collider != null ) 
            {
                bool isRed = sortedLightList[6].lightPhase == 2;
                bool isYellow = sortedLightList[6].lightPhase == 1;
                
                // Debug.Log("inStopRange true block");
                
                    float distanceToOther = hit.distance;
                    // Debug.Log(hit.transform);
                    if(hit.collider.gameObject.tag == "Car")
                    {
                        // Debug.Log(hit.collider.gameObject.tag);
                        // Debug.Log("jIOWEFJIOWFJIEWFJEWIOFJWEIOFJEWIOFJWEIOJFWEIOJFEIO");
                        

                        if(distanceToOther <= carStopDistance)
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
                            //Debug.Log("THE CAR WILL NOW BE STOPPED AHAHAHAHAHAHAHAH");
                            stopCar();
                        }
                        else
                        {
                            //Debug.Log("car started");
                            // startCar();
                            graduallyStartCar();
                        }
                    }
                
                
            }
            else
            {
                // startCar();
                graduallyStartCar();
            }
        
        }
    }


    private void graduallyStartCar()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.linearVelocity = new Vector2(0, 0);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, speed*Vector2.left, acelerationRate*Time.fixedDeltaTime); 

    }
    private void stopCar()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.linearVelocity = new Vector2(0, 0);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, decelerationRate*Time.fixedDeltaTime); //Stop car from moving
        rb.angularVelocity = 0f;
    }

    // void OnDrawGizmos()
    // {
    //     // Visualize the ray in the Scene view.
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(transform.transform.position + new Vector3(-offset,0f,0f), Vector2.left * carStopDistance);
    // }

}
