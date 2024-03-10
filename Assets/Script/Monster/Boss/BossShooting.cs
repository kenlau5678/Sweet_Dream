using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public Transform player;
    private Animator BossAnimator;
    private SpriteRenderer gunRenderer;
    Boss boss;
    public Transform FirePoint;
    public LineRenderer LaserPrefab;
    float DefaultLength = 20f;
    public float laserDamage = 20f;
    public float laserDuration = 0.05f;
    
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

                //Shoot();
            }
        }
        else
        {
            gunRenderer.enabled = false;
        }
    }
    /*void Shoot()
    {
            // 获取枪口位置和方向
        Vector3 gunPosition = FirePoint.position; // 你需要替换成实际的枪口位置
        Vector3 gunDirection = FirePoint.right; 

        // 创建射线
        RaycastHit2D hit = Physics2D.Raycast(gunPosition, gunDirection);

         // 实例化镭射 Prefab
        LineRenderer laser = Instantiate(LaserPrefab, gunPosition, Quaternion.identity);

        // 处理射中的逻辑
        if (hit.collider != null)
        {
            // 射中了物体，你可以在这里处理伤害逻辑
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            if(hit.collider.CompareTag("Player"))
            {
                Debug.Log("TakeDamage");
               // PlayerScript playerScript = hit.collider.GetComponent<PlayerScript>();//获取玩家的脚本
               // playerScript.TakeDamage(laserDamage);//调用玩家脚本的受伤函数
            }

           
            laser.SetPosition(1, hit.point);

        }
        else
        {
            // 如果没有射中物体，设置默认的终点
            laser.SetPosition(1, gunPosition + gunDirection * DefaultLength);
        }
        laser.enabled = true;
        // 启动协程，延迟销毁镭射
        StartCoroutine(DestroyLaserAfterDelay(laser, laserDuration));
    }
    IEnumerator DestroyLaserAfterDelay(LineRenderer laser, float delay)
    {
        yield return new WaitForSeconds(delay);
        // 在延迟之后销毁镭射
        laser.enabled = false;
        Destroy(laser.gameObject);
    }*/
}

 