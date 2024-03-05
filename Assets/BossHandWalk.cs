using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandWalk : StateMachineBehaviour
{
    private int rand;
    public float attackTimer;
    public float minTime;
    public float maxTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rand = Random.Range(0,2);//switch 2 different attack modes
       attackTimer = Random.Range(minTime,maxTime);//random attack time
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
           
       
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

   
}
