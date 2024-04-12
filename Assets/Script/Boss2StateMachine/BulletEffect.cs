using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public float targetScale = 0.15f; // 目标缩放值
    public float duration = 1f; // 动画持续时间

    void Start()
    {
        // 从当前缩放开始，慢慢放大到目标缩放值
        transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
    }
}
