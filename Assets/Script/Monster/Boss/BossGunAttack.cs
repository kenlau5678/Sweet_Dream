using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunAttack : StateMachineBehaviour
{
    public float attackTimer;
    public float minTime = 0.5f;
    public float maxTime = 1.5f;
    private int rand;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0,10);//switch 2 different attack modes
        attackTimer = Random.Range(minTime,maxTime);
        animator.SetBool("isShoot",false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(attackTimer <= 0)
        {
            if(rand <=4)
            {
                animator.SetBool("isGunWalk",true);

            }
            else if(rand <=9){
                animator.SetBool("isShoot",true);
            }
            
        }
        else{
            attackTimer -= Time.deltaTime;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}