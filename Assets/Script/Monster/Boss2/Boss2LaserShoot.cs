using System.Collections;
using UnityEngine;

public class Boss2LaserShoot : MonoBehaviour
{
    public GameObject warningLaserPrefab; // 预警镭射预制体
    public GameObject damageLaserPrefab; // 伤害镭射预制体
    public Transform shootPoint; // 射击点
    public Transform player; // 玩家对象

    private float warningDuration = 2f; // 预警持续时间
    private Vector3 lastAimPosition; // 最后一次瞄准的坐标
    private GameObject damageLaser; // 伤害镭射对象

    public bool shootCheck = false;

    public int LaserShootCounter = 0; //技能counter
 

    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void ShootCiteFunc()//供动画脚本引用
    {
        LaserShootCounter = 0; 
        shootCheck = true;
        StartCoroutine(ShootLaser());
    }
    IEnumerator ShootLaser()
    {
        while (shootCheck)
        {
            LaserShootCounter++;
            if (LaserShootCounter >= 3) //镭射持续次数
            {
                shootCheck = false;
            }

            // 生成预警镭射
            GameObject warningLaser = Instantiate(warningLaserPrefab, shootPoint.position, Quaternion.identity);
            float timer = warningDuration; // 设置计时器为预警持续时间
            float angle = 0f; // 初始化 angle

            while (timer > 0)
            {
                // 让预警镭射朝向玩家的当前坐标
                Vector3 directionToPlayer = player.position - warningLaser.transform.position;
                angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                angle += 90; // 或者 angle += 90; 根据实际情况调整
                warningLaser.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

                yield return null; // 持续更新预警镭射朝向
                timer -= Time.deltaTime; // 减去时间流逝
            }

            // 等待一段时间后发射
            yield return new WaitForSeconds(0.2f); // 这里的 0.xf 表示等待的时间，你可以根据需要调整

            // 销毁预警镭射
            Destroy(warningLaser);

            // 记录最后一刻的坐标
            lastAimPosition = player.position;

            // 生成伤害镭射
            damageLaser = Instantiate(damageLaserPrefab, shootPoint.position, Quaternion.identity);

            Vector3 damageDirection = lastAimPosition - shootPoint.position;
            // 可能需要调整角度修正
            float damageAngle = angle; // 使用预警镭射的最终角度
            damageLaser.transform.localRotation = Quaternion.Euler(0f, 0f, damageAngle);

            // 等待一段时间后继续下一次循环
            yield return new WaitForSeconds(1.5f); // 下一次发射间隔
            // 销毁之前生成的伤害镭射
            if (damageLaser != null)
                {
                    Destroy(damageLaser);
                    
                }
            
        }

    }
}
