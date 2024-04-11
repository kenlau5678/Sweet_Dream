using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BreathingLight : MonoBehaviour
{
    public Light2D breathingLight; // �B�ӵ���� Light2D ����M��
    public float minIntensity = 0.5f; // ��С����
    public float maxIntensity = 2.0f; // ��󏊶�
    public float breathSpeed = 1.0f; // �����ٶ�

    private float targetIntensity; // Ŀ�ˏ���

    void Start()
    {
        // �O�ó�ʼĿ�ˏ��Ȟ���С����
        targetIntensity = minIntensity;
    }

    void Update()
    {
        // ʹ�� Mathf.PingPong ��������С���Ⱥ���󏊶�֮�gѭ�h׃��
        targetIntensity = Mathf.PingPong(Time.time * breathSpeed, maxIntensity - minIntensity) + minIntensity;

        // ��Ŀ�ˏ��ȑ��õ���Դ��
        breathingLight.intensity = targetIntensity;
    }
}
