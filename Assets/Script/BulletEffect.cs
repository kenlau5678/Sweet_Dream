using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public float targetScale = 0.15f; // Ŀ������ֵ
    public float duration = 1f; // ��������ʱ��

    void Start()
    {
        // �ӵ�ǰ���ſ�ʼ�������Ŵ�Ŀ������ֵ
        transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad);
    }
}
