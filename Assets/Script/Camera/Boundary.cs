using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ���ұ��Ϊ "Virtual Camera" �����������
        GameObject virtualCameraObj = GameObject.FindWithTag("Virtual Camera");
        if (virtualCameraObj != null)
        {
            Debug.Log(1);
            // ��ȡ����������� CinemachineConfiner ���
            CinemachineConfiner2D confiner = virtualCameraObj.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                Debug.Log(23);
                // ��ȡ Boundary �� Collider2D ��������� CinemachineConfiner �� BoundingShape2D ����
                Collider2D collider = GetComponent<CompositeCollider2D>();
                if (collider != null)
                {
                    confiner.m_BoundingShape2D = collider;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
