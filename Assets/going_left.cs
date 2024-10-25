using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Callbacks;

public class goLeftScript : MonoBehaviour
{
    float speed = 5f;
    [SerializeField] left_traffic_light leftTrafficLight;
    bool startedCollision = false;


    void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        speed = 5f;
        startCar();
        gameObject.AddComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) //called when collision begins
    {
        Rigidbody2D thisrb = GetComponent<Rigidbody2D>(); 
        Rigidbody2D otherRb = collision.rigidbody;

        if (thisrb.position.x > otherRb.position.x) //compares velocities of collided objs
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
        if(collision.transform.position.x < this.transform.position.x)
        {
             //signifies that car is no longer starting a collision
            // Debug.Log("Setting startedCollision to false");
            stopCar();
        }
    }
    
    void OnCollisionExit2D(Collision2D collision) //called when cars seperate (forward car moves on intersection)
    {
        
        if(collision.transform.position.x < this.transform.position.x)
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
        if(-21 >= transform.position.x) 
        {
            transform.position = new Vector3(21, 2, 0);
        }
        // Debug.Log("objects updating");
        if(inStopRange()) 
        {
            // Debug.Log("inStopRange true block");
            if(leftTrafficLight.isGreenLight()) 
            {
                // Debug.Log("greenlight block");
                startCar();
            }
            else if(leftTrafficLight.isRedLight())
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

        return 6f <= transform.position.x && transform.position.x <= 7.7f;
    }

    private void startCar()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Debug.Log(rb.name+" is starting with speed="+speed);
        rb.velocity = new Vector2(-speed, 0);  
        // Debug.Log(rb.name+" v="+rb.velocity);
    }

    private void stopCar()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;  // Stop the object from moving
        rb.angularVelocity = 0f;
    }

}
