using System.Collections.Generic;
using UnityEngine;

public class GroundDistanceCheck : MonoBehaviour
{
    public Transform footPoint; // �Ų�λ�õ�
    public LayerMask groundLayer; // ����ͼ��
    public float maxDistance = 100f; // ������

    public Transform currentGroundObject; // ��ǰ����������
    public Transform previousGroundObject; // ��һ������������

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
