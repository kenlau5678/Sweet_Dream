using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 查找标记为 "Virtual Camera" 的虚拟摄像机
        GameObject virtualCameraObj = GameObject.FindWithTag("Virtual Camera");
        if (virtualCameraObj != null)
        {
            Debug.Log(1);
            // 获取虚拟摄像机的 CinemachineConfiner 组件
            CinemachineConfiner2D confiner = virtualCameraObj.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                Debug.Log(23);
                // 获取 Boundary 的 Collider2D 组件并赋给 CinemachineConfiner 的 BoundingShape2D 属性
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
