using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D col;


    // Start is called before the first frame update
    void Start()
    {
        col.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectedObjs.Add(collision);
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectedObjs.Remove(collision);
        }
    }
    private void OnDrawGizmos()
    {
        if (col != null && col is CircleCollider2D)
        {
            CircleCollider2D circleCollider = (CircleCollider2D)col;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius); // Draw helper line for AttackPoint
        }
    }
}