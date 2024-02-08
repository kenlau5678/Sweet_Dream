using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmit : MonoBehaviour
{
    public Transform Point;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PlayerMovement>().canTransmit > 1)
        {
            collision.transform.position = Point.transform.position;
            collision.GetComponent<PlayerMovement>().canTransmit = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<PlayerMovement>().canTransmit +=1;
    }
}