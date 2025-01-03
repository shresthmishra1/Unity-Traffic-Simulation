using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goUpScript : MonoBehaviour
{
    float speed = 10f;
    [SerializeField] up_traffic_light trafficlight;
    bool startedCollision = false;


    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        float speed = UnityEngine.Random.Range(2.5f, 7.5f);
        startCar();
        gameObject.AddComponent<BoxCollider2D>();
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
            transform.position = new Vector3(2.2f, -21f, 0);
        }
        // Debug.Log("objects updating");
        if(inStopRange()) 
        {
            // Debug.Log("inStopRange true block");
            if(trafficlight.isGreenLight()) 
            {
                // Debug.Log("greenlight block");
                startCar();
            }
            else if(trafficlight.isRedLight())
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
        rb.linearVelocity = Vector2.zero;  // Stop the object from moving
        rb.angularVelocity = 0f;
    }

}
