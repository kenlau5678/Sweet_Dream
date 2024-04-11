using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTP : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    public float distanceToFront = 10f; // 瞬移时Boss应该出现在玩家前方的距离

    void Update()
    {
        // 假设你想要在按下'Q'键时触发瞬移
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TeleportToFrontOfPlayer();
        }
    }

    public void TeleportToFrontOfPlayer()
    {
        // 计算玩家前方的位置
        Vector3 playerFront = player.right * distanceToFront; 
        Vector3 teleportPosition = player.position + playerFront;

        // 设置Boss的位置为计算出的位置
        transform.position = new Vector3(teleportPosition.x, teleportPosition.y, 0); // 确保Z轴为0

        // 瞬移的粒子效果
        // Instantiate(teleportEffect, teleportPosition, Quaternion.identity);
    }
}