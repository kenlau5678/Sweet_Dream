using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    private Animator BossAnimator;
    private SpriteRenderer gunRenderer;
    Boss boss;
    void Start()
    {
        // 获取Animator和SpriteRenderer组件
        BossAnimator = GetComponentInParent<Animator>();
        gunRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        bool isShoot = BossAnimator.GetBool("isShoot");

        // 根据布尔值控制枪的可见性
        if (isShoot)
        {
            // 显示枪
            gunRenderer.enabled = true;
            if (player != null)
            {
                // 计算玩家与怪物手臂节点之间的方向
                Vector3 directionToPlayer = player.position - transform.position;

                // 计算方向对应的旋转角度
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg + 180;

                // 将手臂节点朝向玩家位置
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else
        {
            gunRenderer.enabled = false;
        }
    }
}

 