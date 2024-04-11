using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaveAttack : MonoBehaviour
{
    public GameObject WavePrefab; // 圆的预制体
    public Transform startPoint; 
    public float expandSpeed = 0.1f; // 初始扩大速度
    public float acceleration = 8f;
    private float currentExpandSpeed;
    public float maxRadius = 15f; // 最大半径
    public float intensity;
    public float shaketime;
    public float frequency;

    void Start()
    {
        currentExpandSpeed = expandSpeed;
        // 在Boss头部生成圆
        //GameObject circle = Instantiate(WavePrefab, startPoint.position, Quaternion.identity);
        
    }
    public void SpawnCircle() //公共调用时生成圆
    {
        // 在Boss头部生成圆
        GameObject circle = Instantiate(WavePrefab, startPoint.position, Quaternion.identity);
        CameraShake.Instance.shakeCameraWithFrequency(intensity, frequency, shaketime);
        // 开始逐渐扩大圆的半径
        StartCoroutine(ExpandCircle(circle.transform));
        
    }
    IEnumerator ExpandCircle(Transform circleTransform)
    {
        currentExpandSpeed = expandSpeed;
        float radius = 0f;
        while (radius < maxRadius)
        {
            
            radius += currentExpandSpeed * Time.deltaTime;
            // 设置圆的半径
            circleTransform.localScale = new Vector3(radius, radius, 1f);
            // 增加扩大速度
            currentExpandSpeed += acceleration * Time.deltaTime;
            yield return null;
        }

        // 销毁圆
        Destroy(circleTransform.gameObject);
    }

    
}