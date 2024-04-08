using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2LaserShoot : MonoBehaviour
{
    public Transform player; // 玩家的 Transform
    public Transform leftShootPoint; // 左射击点
    public Transform rightShootPoint; // 右射击点
    private Transform shootPoint;
    public LineRenderer warningLine; // 预警线
    public GameObject laserPrefab; // 镭射预制体
    public float chargeDuration = 1f; // 蓄力持续时间

    private Vector3 shootPosition; // 镭射射击位置
    private bool isCharging = false; // 是否正在蓄力
    private float chargeTimer = 0f; // 蓄力计时器

    void Start()
    {
        // 初始化预警线
        warningLine.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 随机选择左右射击点
        shootPoint = Random.Range(0, 2) == 0 ? leftShootPoint : rightShootPoint;
    }

    void Update()
    {
        if (!isCharging)
        {
            // 进入蓄力阶段
            Charge();
        }
        else
        {
            // 蓄力阶段持续时间
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeDuration)
            {
                // 进入射击阶段
                Shoot();
            }
            else
            {
                // 实时跟踪玩家位置并更新预警线
                UpdateWarningLine();
            }
        }
    }

    void Charge()
    {
        // 显示预警线
        warningLine.enabled = true;

        // 更新预警线位置
        UpdateWarningLine();

        // 进入蓄力阶段
        isCharging = true;
        chargeTimer = 0f;
    }

    void Shoot()
    {
        // 隐藏预警线
        warningLine.enabled = false;

        // 生成镭射并设置射击位置
        GameObject laser = Instantiate(laserPrefab, shootPosition, Quaternion.identity);

        // 进入冷却阶段
        isCharging = false;
        if (chargeTimer >= chargeDuration)
            {
                Destroy(laser);
            }
        
    }

    void UpdateWarningLine()
    {
        // 计算射击位置
        shootPosition = player.position;

        // 更新预警线位置
        warningLine.SetPosition(0, shootPoint.position);
        warningLine.SetPosition(1, shootPosition);
    }
}
