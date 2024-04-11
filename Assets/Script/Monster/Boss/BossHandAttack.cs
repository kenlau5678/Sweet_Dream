using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandAttack : StateMachineBehaviour
{
    public float attackTimer;
    public float timeDuration = 0.5f;
    private int rand;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(0,10);//switch 3 different attack modes
        attackTimer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if(attackTimer <= 0)
        // {
            // if(rand <=3)
            // {
            //     animator.SetTrigger("ToGunAttack");
            //     animator.SetBool("isHandWalk",false);           
            // }
            // else{
                
                
           // }
            //attackTimer = 1f;//reset timer
        attackTimer += Time.deltaTime;
        if(attackTimer >= timeDuration)
        {
            animator.SetBool("isHandWalk",true);
        }
            
       //}
        // else{
        //     attackTimer -= Time.deltaTime;
        // }
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer = 0f;
    }

   
}
