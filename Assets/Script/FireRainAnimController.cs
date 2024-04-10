using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRainAnimController : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject fireBallManager = GameObject.Find("FireBallManager");
        if (fireBallManager != null)
        {
            FireBallManager fireBall = fireBallManager.GetComponent<FireBallManager>();
            if (fireBall != null)
           {
                fireBall.SpawnFireBall();
            }
            else
            {
                Debug.LogWarning("Not found on fireBallManager object.");
            }
            
        }
        else
        {
            Debug.LogWarning("FireBallManager object not found.");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    
}
