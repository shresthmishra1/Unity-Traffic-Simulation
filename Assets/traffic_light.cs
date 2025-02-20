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

    public bool isGreenLight(){
        return !isRed;
    }

    public bool isRedLight(){
        return isRed;
    }


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(isYellow)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = yellowlight;
        }
        else if(isRed)
        {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = redlight;
        }
        else
        {
        	this.gameObject.GetComponent<SpriteRenderer>().sprite = greenlight;
        }
    }
}

