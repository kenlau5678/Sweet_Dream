using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Shooting : MonoBehaviour
{
    public Transform leftShootPoint; // 左射击点
    public Transform rightShootPoint; // 右射击点
    public GameObject laserPrefab; // 镭射预制体

    private GameObject currentLaser; // 当前的镭射对象

    void Start()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        // 随机选择左右射击点
        Transform shootPoint = Random.Range(0, 2) == 0 ? leftShootPoint : rightShootPoint;

        // 生成镭射
        currentLaser = Instantiate(laserPrefab, shootPoint.position, Quaternion.identity);

        // 计算镭射的旋转角度
        Vector3 directionToPlayer = (shootPoint.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
        currentLaser.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 启动镭射旋转
        RotateLaser();
    }

    void RotateLaser()
    {
        if (currentLaser != null)
        {
            // 旋转镭射
            currentLaser.transform.Rotate(Vector3.forward * Time.deltaTime * 50f);

            // 继续旋转
            Invoke("RotateLaser", 0f);
        }
    }
}
    


 