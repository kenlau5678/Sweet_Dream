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
    Monster monster;
    Transform player;
	Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rand = Random.Range(0,10);//switch 2 different attack modes
       attackTimer = Random.Range(minTime,maxTime);//random attack time
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<Boss>();
       monster = animator.GetComponent<Monster>();
       animator.SetBool("isShoot",false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isShoot",false);
       if(attackTimer <= 0)
        {
            if(rand <=1)//10% switch to hand attack
            {
                animator.SetTrigger("ToHandAttack");
            }
            else 
            {
                animator.SetBool("isShoot",true);
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
        animator.SetBool("isGunWalk",false);
        animator.ResetTrigger("ToHandAttack");
    }

    
}
