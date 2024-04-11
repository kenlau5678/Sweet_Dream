using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandWalk : StateMachineBehaviour
{
    private int rand;
    public float attackTimer;
    public float minTime = 1f;
    public float maxTime = 2f;

    public float distanceToPlayer;
    public float speed = 5f;
    public float shootRange = 8f;
    public float dashRange = 3f;
    Boss boss;
    Monster monster;
    Transform player;
	Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    //    rand = Random.Range(0,2);//switch 2 different attack modes
       attackTimer = Random.Range(minTime,maxTime);//random attack time
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<Boss>();
       monster = animator.GetComponent<Monster>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //移动功能
        distanceToPlayer = Vector2.Distance(animator.transform.position, player.position);
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
	    rb.MovePosition(newPos);
        
    //    if(attackTimer <= 0)
    //     {
            if(distanceToPlayer >= shootRange )
            {
                Debug.Log("GUNNNN");
                animator.SetTrigger("ToGunAttack");
            }
            if(distanceToPlayer <= dashRange )
            {
                Debug.Log("dash");
                animator.SetTrigger("ToHandAttack");
            }
            //若在 dashrange ~ shootRange,则走路
        // }
        // else{
        //     attackTimer -= Time.deltaTime;
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("isHandWalk",false);
       attackTimer = 2f;
    }

   
}
