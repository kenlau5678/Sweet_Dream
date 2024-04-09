using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2LaserShoot : MonoBehaviour
{
    public GameObject warningLaserPrefab; // 预警镭射预制体
    public GameObject damageLaserPrefab; // 伤害镭射预制体
    public Transform shootPoint; // 射击点
    public Transform player; // 玩家对象

    private Vector3 lastAimPosition; // 最后一次瞄准的坐标

    void Start()
    {
        StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
         // 生成预警镭射
        GameObject warningLaser = Instantiate(warningLaserPrefab, shootPoint.position, Quaternion.identity);

        while (true)
        {
            // 让预警镭射朝向玩家的当前坐标
            Vector3 directionToPlayer = player.position - shootPoint.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            warningLaser.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return null; // 等待下一帧更新

            // 如果预警镭射朝向玩家，则结束循环
            if (Mathf.Abs(warningLaser.transform.eulerAngles.z - angle) < 0.5f)
                break;
        }

        // 销毁预警镭射
        Destroy(warningLaser);

        // 记录最后一刻的坐标
        lastAimPosition = player.position;

        // 销毁预警镭射
        Destroy(warningLaser);

        

        // 生成伤害镭射
        GameObject damageLaser = Instantiate(damageLaserPrefab, shootPoint.position, Quaternion.identity);

        Vector3 damageDirection = lastAimPosition - shootPoint.position;
        // 设置伤害镭射的终点为记录下来的坐标
        damageLaser.transform.up = damageDirection.normalized;
    }
}