using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAiming : StateMachineBehaviour
{
    public float attackTimer;
    public float minTime = 1f;
    public float maxTime = 2f;
    private int rand;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0,10);//switch 2 different attack modes
        attackTimer = Random.Range(minTime,maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attackTimer <= 0)
        {
            animator.SetBool("isGunWalk",true);
        }
        else{
            animator.SetBool("isGunWalk",false);
            attackTimer -= Time.deltaTime;
        }  
        
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("isShoot",false);
       attackTimer = 1f;
    }

    
}
