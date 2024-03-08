using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmit : MonoBehaviour
{
    public Transform Point;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.GetComponent<PlayerMovement>().canTransmit);
        if (collision.tag == "Player" && collision.GetComponent<PlayerMovement>().canTransmit ==2)
        {
           
            collision.GetComponent<PlayerMovement>().canTransmit = 0;
           
            collision.transform.position = Point.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().canTransmit += 1;
        }
           
    }
}