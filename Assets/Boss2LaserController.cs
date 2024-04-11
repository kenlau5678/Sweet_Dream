using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2LaserController : StateMachineBehaviour
{
    public GameObject boss2;
    public float timer;
    float timeDuration = 8.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
       // 获取 Boss 对象的引用
        boss2 = GameObject.Find("Boss2");
        if (boss2 != null)
        {
            Boss2LaserShoot boss2LaserShoot = boss2.GetComponent<Boss2LaserShoot>();
            if (boss2LaserShoot != null)
            {
                boss2LaserShoot.ShootCiteFunc();
                
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
        timer += Time.deltaTime;
        if (timer >= timeDuration)
        {
            animator.SetBool("isLaser", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
