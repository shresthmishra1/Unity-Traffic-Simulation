using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traffic_light : MonoBehaviour
{
    public Sprite greenlight;
    public Sprite redlight;
    public Sprite yellowlight;
    public bool isRed = true;
    public bool isYellow = false;
    public int light_number;


    public float greenTime;  // Duration for green light
    public float redTime;    // Duration for red light
    public float yellowTime; // Duration for yellow light

    public int lightPhase; // 0:green   1:yellow   2:red

    public bool isGreenLight(){
        return lightPhase == 0;
    }

    public bool isRedLight(){
        return lightPhase == 2;
    }


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        GameObject mainObj = GameObject.FindGameObjectWithTag("MainCamera");
        main mainscript = mainObj.GetComponent<main>();
        bool timeup = mainscript.isTimerComplete;
        if(lightPhase == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = yellowlight;
            if(!timeup)
            {
                yellowTime += Time.deltaTime;
            }
        }
        else if(lightPhase == 2)
        {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = redlight;
            if(!timeup)
            {
                redTime += Time.deltaTime;
            }
        }
        else
        {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = greenlight;
            if(!timeup)
            {
                greenTime += Time.deltaTime;
            }
        }
    }
}

