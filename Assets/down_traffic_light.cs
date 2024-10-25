using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_traffic_light : MonoBehaviour
{
    public Sprite greenlight;
    public Sprite redlight;
    public static bool isRed = true;
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
        
        if(i % 1000 == 0)
        {
            isRed = !isRed;
            prevRed = isRed;
        }
        
        if(1000< i && i % 1000 <= 300)
        {
            isRed = true;
        }
        else if(1000< i && i%1000 > 300)
        {
            isRed = prevRed;
        }
        if(isRed)
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
