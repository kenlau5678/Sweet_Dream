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
    private float NevSpeed = 1f;
    monsterRun monsterrun;
    
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
        // if(monsterrun.isPatrol)
		// {
             Patrol();
        // }
    }
    private void Patrol()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == NevPointB.transform)
        {
            rb.velocity = new Vector2(NevSpeed,0);
        }
        else
        {
            rb.velocity = new Vector2(-NevSpeed,0);//往左走
        }
        if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == NevPointB.transform)//往点B巡逻
        {
            flip();
            currentPoint = NevPointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == NevPointA.transform)//往点A巡逻
        {
            flip();
            currentPoint = NevPointB.transform;
        }
    }
    private void flip()//monster flip翻转
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos() //virsualize the empty objects
    {
        Gizmos.DrawWireSphere(NevPointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(NevPointB.transform.position, 0.5f);//将emptyObject可视化
        Gizmos.DrawLine(NevPointA.transform.position,NevPointB.transform.position);
    }
}
