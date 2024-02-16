using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hourglass : MonoBehaviour
{
    public GameObject hourhand;
    public GameObject minutehand;
    public float hourAngles;
    public float minuteAngles;
    int isstay = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isstay = 1;
            Debug.Log(isstay);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isstay = 0;
            Debug.Log(isstay);
        }
        
    }

    private void Update()
    {
        if (isstay ==1 && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("OK");
            StartCoroutine(hourhand.GetComponent<HandRotation>().RotationCoroutine());
            StartCoroutine(minutehand.GetComponent<HandRotation>().RotationCoroutine());

        }
    }
}
