using System.Collections.Generic;
using UnityEngine;

public class GroundDistanceCheck : MonoBehaviour
{
    public LayerMask groundLayer; // ����ȷ����Щ�㱻��Ϊ�����LayerMask
    public float triggerDistance = 1.0f; // �����ض������ľ�����ֵ

    public Vector3 lastFeetPointPosition = Vector3.zero; // ��һ���Ӵ����λ��
    private bool hasLastPoint = false; // �Ƿ��Ѿ���¼����һ���Ӵ���
    public float intensity;
    public float shaketime;
    public float frequency;
    public Transform feetPos;
    public float checkRadius;
    void Update()
    {
        
        // �� FeetPoint �������߼����Ѱ�ҵ���
        if (Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer))
        {
            if (hasLastPoint)
            {
                float distance = lastFeetPointPosition.y-feetPos.position.y;
                if (distance > triggerDistance)
                {
                    // �����������ֵʱ�����ض�����
                    CameraShake.Instance.shakeCameraWithFrequency(intensity, frequency, shaketime);
                }
            }

            // ������һ���Ӵ����λ��
            lastFeetPointPosition = feetPos.position;
            hasLastPoint = true;
        }
    }

}
