using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRainAnimController : StateMachineBehaviour
{
    public GameObject fireBallManager;
    float timer;
    float timeDuration = 3f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(fireBallManager);
        if (fireBallManager != null)
        {
            fireBallManager.SetActive(true);
        }
        else
        {
            Debug.LogWarning("FireBallManager object not found.");
        }
        timer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer >= timeDuration)
        {
            animator.SetTrigger("Idle");
        }
    }
    // 在状态退出时禁用 FireBallManager
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (fireBallManager != null)
        // {
        //     fireBallManager.SetActive(false);
        // }
        animator.SetBool("FireRain",true);
    }

    
}
