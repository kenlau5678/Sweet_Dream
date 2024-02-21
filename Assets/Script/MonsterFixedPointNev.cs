using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFixedPointNev : MonoBehaviour
{
    public GameObject NevPointA;
    public GameObject NevPointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = NevPointB.transform;
        //anim.SetBool("isRunning",true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == NevPointB.transform)
        {
            rb.velocity = new Vector2(speed,0);
        }
        else
        {
            rb.velocity = new Vector2(-speed,0);//往左走
        }
        if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == NevPointB.transform)//往点A巡逻
        {
            currentPoint = NevPointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == NevPointA.transform)//往点A巡逻
        {
            currentPoint = NevPointB.transform;
        }
    }
}
