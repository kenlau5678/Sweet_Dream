using System.Collections.Generic;
using UnityEngine;

public class GroundDistanceCheck : MonoBehaviour
{
    public LayerMask groundLayer; // 用于确定哪些层被视为地面的LayerMask
    public float triggerDistance = 1.0f; // 触发特定函数的距离阈值

    public Vector3 lastFeetPointPosition = Vector3.zero; // 上一个接触点的位置
    private bool hasLastPoint = false; // 是否已经记录了上一个接触点
    public float intensity;
    public float shaketime;
    public float frequency;
    public Transform feetPos;
    public float checkRadius;
    void Update()
    {
        
        // 从 FeetPoint 向下射线检测以寻找地面
        if (Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer))
        {
            if (hasLastPoint)
            {
                float distance = lastFeetPointPosition.y-feetPos.position.y;
                if (distance > triggerDistance)
                {
                    // 当距离大于阈值时触发特定函数
                    CameraShake.Instance.shakeCameraWithFrequency(intensity, frequency, shaketime);
                }
            }

            // 更新上一个接触点的位置
            lastFeetPointPosition = feetPos.position;
            hasLastPoint = true;
        }
    }

}
