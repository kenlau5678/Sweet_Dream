using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    private Animator BossAnimator;
    private SpriteRenderer gunRenderer;
    Boss boss;
    public Transform FirePoint;
    public GameObject LaserPrefab;
    float DefaultLength = 20f;
    
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

                Shoot();
            }
        }
        else
        {
            gunRenderer.enabled = false;
        }
    }
    void Shoot()
    {
            // 获取枪口位置和方向
        Vector3 gunPosition = FirePoint.position; // 你需要替换成实际的枪口位置
        Vector3 gunDirection = FirePoint.right; 

        // 创建射线
        RaycastHit2D hit = Physics2D.Raycast(gunPosition, gunDirection);

        // 实例化镭射 Prefab
        GameObject laser = Instantiate(LaserPrefab, gunPosition, Quaternion.identity);

        // 处理射中的逻辑
        if (hit.collider != null)
        {
            // 射中了物体，你可以在这里处理伤害逻辑
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            laser.transform.position = hit.point;

        }
        else
        {
            // 如果没有射中物体，设置默认的终点
            laser.transform.position = gunPosition + gunDirection * DefaultLength;
        }
    }
}

 