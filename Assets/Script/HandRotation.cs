using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation: MonoBehaviour
{
    public Transform clockCenter; // 指定時鐘中心的Transform
    public float duration = 1.0f; // 旋轉持續時間，以秒為單位
    public float elapsedTime = 0.0f; // 經過的時間
    public float startAngle = 0.0f; // 開始時的角度
    public float targetAngle = 90.0f; // 目標角度，順時針旋轉90度

    void Start()
    {
    }

    void Update()
    {
        
    }

    public IEnumerator RotationCoroutine()
    {
        elapsedTime = 0.0f;
        float startAngle = transform.eulerAngles.z; // 记录开始时的角度
        float endAngle = startAngle + targetAngle; // 计算结束时的目标角度
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // 更新經過的時間
            float currentAngle = Mathf.Lerp(startAngle, startAngle + targetAngle, elapsedTime / duration);
            transform.RotateAround(clockCenter.position, Vector3.forward, currentAngle - transform.eulerAngles.z);
            yield return null;
        }
        transform.eulerAngles = new Vector3(0, 0, endAngle);
    }
}
