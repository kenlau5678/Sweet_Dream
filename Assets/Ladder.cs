using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
                collision.GetComponent<Rigidbody2D>().gravityScale = 0;
                collision.GetComponent<PlayerMovement>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 1;
            collision.GetComponent<PlayerMovement>().isTrigger = false;
        }
    }
}
