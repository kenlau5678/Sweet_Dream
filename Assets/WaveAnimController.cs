using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnimController : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // 获取 Boss 对象的引用
        GameObject boss2 = GameObject.Find("Boss2");
        if (boss2 != null)
        {
            BossWaveAttack bosswaveAttack = boss2.GetComponent<BossWaveAttack>();
            if (boss2.GetComponent<Boss2>() != null)
           {
                boss2.GetComponent<BossWaveAttack>().SpawnCircle();
            }
            else
            {
                Debug.LogWarning("Boss2 script not found on Boss2 object.");
            }
            
        }
        else
        {
            Debug.LogWarning("Boss2 object not found.");
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
