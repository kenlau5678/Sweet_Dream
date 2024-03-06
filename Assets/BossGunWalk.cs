using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunWalk : StateMachineBehaviour
{
    private int rand;
    public float attackTimer;
    public float minTime = 2f;
    public float maxTime = 3f;

    public float distanceToPlayer;
    public float speed;
    Boss boss;
    Transform player;
	Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rand = Random.Range(0,2);//switch 2 different attack modes
       attackTimer = Random.Range(minTime,maxTime);//random attack time
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(attackTimer <= 0)
        {
            if(rand == 0)
            {
                    animator.SetTrigger("ToHandAttack");
            }
            else
            {
                    animator.SetTrigger("ToGunAttack");
            }
        }
        else{
            attackTimer -= Time.deltaTime;
        }
        //移动模组
        distanceToPlayer = Vector2.Distance(animator.transform.position, player.position);
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
	    rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    
}