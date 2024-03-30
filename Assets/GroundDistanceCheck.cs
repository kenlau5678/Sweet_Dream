using System.Collections.Generic;
using UnityEngine;

public class GroundDistanceCheck : MonoBehaviour
{
    public Transform footPoint; // 脚部位置点
    public LayerMask groundLayer; // 地面图层
    public float maxDistance = 100f; // 最大距离

    public Transform currentGroundObject; // 当前碰到的物体
    public Transform previousGroundObject; // 上一个碰到的物体

    void Update()
    {
        currentGroundObject = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(footPoint.position, 0.1f, groundLayer);

        foreach (Collider2D collider in colliders)
        {
            currentGroundObject = collider.transform;
        }

        if (currentGroundObject != null)
        {
            Debug.Log("Current object: " + currentGroundObject.name);

            if (previousGroundObject != null && currentGroundObject != previousGroundObject)
            {
                Debug.Log("Previous object: " + previousGroundObject.name);
            }

            previousGroundObject = currentGroundObject;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(footPoint.position, 0.1f);
    }
}
