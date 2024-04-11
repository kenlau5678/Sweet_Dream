using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2idle : StateMachineBehaviour
{
    bool isWave = false;
    bool isLaser = false;
    public int idleCount = 0;
    public float timer;
    public float timeDuration = 3f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       idleCount++;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      timer += Time.deltaTime;
       if(idleCount == 1)
       { animator.SetBool("isLaser", true);}
       if (idleCount >= 4 || timer >= timeDuration)
       {
         isWave = true;
       } 
       if(isWave)
       {   animator.SetTrigger("Wave");}

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       isLaser = false;
       isWave = false;
       timer = 0f;//Reset
    }

    
}
