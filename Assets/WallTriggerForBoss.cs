using System.Collections;
using UnityEngine;
using DG.Tweening;

public class WallTriggerForBoss : MonoBehaviour
{
    public GameObject BossPrefab; // Boss的预制体
    private bool hasTriggered = false; // 添加一个标志来确保只触发一次

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // 设置标志，防止重复触发

            // 移动墙壁
            transform.DOMoveY(104, 1f).SetEase(Ease.OutQuad);

            // 生成Boss
            if (BossPrefab != null)
            {
                // 实例化Boss的预制体
                GameObject bossInstance = Instantiate(BossPrefab, BossPrefab.transform.position, Quaternion.identity);
                bossInstance.SetActive(true); // 确保Boss是激活状态
            }
        }
    }
}
