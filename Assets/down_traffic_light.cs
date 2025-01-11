using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_traffic_light : MonoBehaviour
{
    public Sprite greenlight;
    public Sprite redlight;
    public Sprite yellowlight;
    public static bool isRed = true;
    public static bool isYellow = false;
    int i = 1;

    public bool isGreenLight(){
        return !isRed;
    }

    public bool isRedLight(){
        return isRed;
    }


    void Start()
    {
        
    }

    bool prevRed = false;
    void Update()
    {
        // if(isGreenLight() && i % 1000 == 0)
        // {
        // 	isRed = !isRed;
        // } 
        // else if(isRedLight() && i % 200 == 0)
        // {
        //     isRed = !isRed;
        // }
        int lightTime = 1000;
        int timeAllRed = 300;
        if(i % lightTime == 0)
        {
            isRed = !isRed;
            prevRed = isRed;
        }
        
        if(lightTime< i && i % lightTime <= timeAllRed)
        {
            if(prevRed == true)
            {
                isYellow = true;
            }
            else
            {
                isRed = true;
            }
        }
        else if(lightTime< i && i%lightTime > timeAllRed)
        {
            isRed = prevRed;
        }
        if(isYellow)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = yellowlight;
            isYellow = false;
        }
        else if(isRed)
        {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = redlight;
        }
        else
        {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = greenlight;
        }
        i ++;
    }

}
