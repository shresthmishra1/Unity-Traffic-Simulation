using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class going_up : MonoBehaviour
{
    float speed = 10f;
    //[SerializeField] right_traffic_light trafficlight;
    bool startedCollision = false;
    // public float detectionDistance = 0.5f; // Distance to detect the car in front.
    public float stopDistance = 2f; // Minimum distance to stop the car a little before.
    public float decelerationRate = 3f; // Rate to slow down smoothly.
    float offset;


    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        float speed = UnityEngine.Random.Range(2.5f, 12.5f);
        startCar();
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.tag = "Car";
        BoxCollider2D boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        Vector2 size = boxCollider.size;
        Vector3 scale = this.transform.localScale;
        Debug.Log(size);
        offset = size.y*0.5f*scale.y+0.000001f;
        // offset = size.x*0.5f;
    }

    void OnCollisionEnter2D(Collision2D collision) //called when collision begins
    {
        Rigidbody2D thisrb = GetComponent<Rigidbody2D>(); 
        Rigidbody2D otherRb = collision.rigidbody;

        if (thisrb.position.y < otherRb.position.y) //compares velocities of collided objs
        {
            startedCollision = true;
            // Debug.Log("i am starting collision");
            stopCar();
        }
        else
        {
            startedCollision = false;
        }
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.position.y > this.transform.position.y)
        {
            stopCar();
        }
    }
    
    void OnCollisionExit2D(Collision2D collision) //called when cars seperate (forward car moves on intersection)
    {
        if(collision.transform.position.y > this.transform.position.y)
        {
            startedCollision = false; //signifies that car is no longer starting a collision
            // Debug.Log("Setting startedCollision to false");
            startCar();
        }

    }
    

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Step 1: resets car position if car goes off screen
        if(transform.position.y >= 21) 
        {
            Destroy(gameObject);
        }
        // Debug.Log("objects updating");
        if(inStopRange()) 
        {
            bool isRed = up_traffic_light.isRed;
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
            int layerNum = 0;
            string layerName = LayerMask.LayerToName(layerNum);
            int layerMask = LayerMask.GetMask(layerName);
            RaycastHit2D hit = Physics2D.Raycast(transform.transform.position + new Vector3(0f,offset,0f), Vector3.up, stopDistance, layerMask);
            if(hit.collider != null )
            {
                // Debug.Log(hit.transform);
                if(hit.collider.gameObject.tag == "Car")
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    Debug.Log("jIOWEFJIOWFJIEWFJEWIOFJWEIOFJEWIOFJWEIOJFWEIOJFEIO");
                    float distanceToOther = hit.distance;

                    if(distanceToOther <= stopDistance)
                    {
                        Debug.Log("THE CAR WILL NOW BE STOPPED AHAHAHAHAHAHAHAH");
                        stopCar();
                    }
                    else
                    {
                        startCar();
                    }
                }
                
            }
            else
            {
                startCar();
            }
        }
    }

    

    private bool inStopRange()
    {

        return -7.7f <= transform.position.y && transform.position.y <= -6f;
    }

    private void startCar()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Debug.Log(rb.name+" is starting with speed="+speed);
        rb.linearVelocity = new Vector2(0, speed);  
        // Debug.Log(rb.name+" v="+rb.velocity);
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
    //     Gizmos.DrawRay(transform.transform.position + new Vector3(0f,offset,0f), Vector2.up * stopDistance);
    // }
}
