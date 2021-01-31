using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformController : MonoBehaviour
{
    PlatformEffector2D affector;
    float waittime = 0.25f;

    private void Start()
    {
        affector = GetComponent<PlatformEffector2D>();


    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            waittime = 0.5f;    
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if(waittime <= 0)
            {
                affector.rotationalOffset = 90f;
                waittime = .5f;

            }
            else
            {
                waittime -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) 
        {
            affector.rotationalOffset = 0;
        }
    }
}
