using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation: MonoBehaviour
{
    public Transform clockCenter; // ָ���r����ĵ�Transform
    public float duration = 1.0f; // ���D���m�r�g��������λ
    public float elapsedTime = 0.0f; // ���^�ĕr�g
    public float startAngle = 0.0f; // �_ʼ�r�ĽǶ�
    public float targetAngle = 90.0f; // Ŀ�˽Ƕȣ�형r����D90��

    void Start()
    {
    }

    void Update()
    {
        
    }

    public IEnumerator RotationCoroutine()
    {
        elapsedTime = 0.0f;
        float startAngle = transform.eulerAngles.z; // ��¼��ʼʱ�ĽǶ�
        float endAngle = startAngle + targetAngle; // �������ʱ��Ŀ��Ƕ�
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // ���½��^�ĕr�g
            float currentAngle = Mathf.Lerp(startAngle, startAngle + targetAngle, elapsedTime / duration);
            transform.RotateAround(clockCenter.position, Vector3.forward, currentAngle - transform.eulerAngles.z);
            yield return null;
        }
        transform.eulerAngles = new Vector3(0, 0, endAngle);
    }
}
