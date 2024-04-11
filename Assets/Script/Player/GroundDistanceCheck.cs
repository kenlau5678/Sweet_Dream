using System.Collections.Generic;
using UnityEngine;

public class GroundDistanceCheck : MonoBehaviour
{
    public LayerMask groundLayer; // ����ȷ����Щ�㱻��Ϊ�����LayerMask
    public float triggerDistance = 1.0f; // �����ض������ľ�����ֵ

    public Vector3 lastFeetPointPosition = Vector3.zero; // ��һ���Ӵ����λ��
    private bool hasLastPoint = false; // �Ƿ��Ѿ���¼����һ���Ӵ���
    public float intensity = 5f;
    public float shaketime = 0.2f;
    public float frequency = 0.2f;
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
                    if(AudioManager.instance!=null)
                    {
                        AudioManager.instance.PlaySFX("Down");
                    }
                }
            }

            // ������һ���Ӵ����λ��
            lastFeetPointPosition = feetPos.position;
            hasLastPoint = true;
        }
    }

}
