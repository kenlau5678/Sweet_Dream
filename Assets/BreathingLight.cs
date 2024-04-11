using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BreathingLight : MonoBehaviour
{
    public Light2D breathingLight; // 連接到你的 Light2D 燈光組件
    public float minIntensity = 0.5f; // 最小強度
    public float maxIntensity = 2.0f; // 最大強度
    public float breathSpeed = 1.0f; // 呼吸速度

    private float targetIntensity; // 目標強度

    void Start()
    {
        // 設置初始目標強度為最小強度
        targetIntensity = minIntensity;
    }

    void Update()
    {
        // 使用 Mathf.PingPong 函數在最小強度和最大強度之間循環變化
        targetIntensity = Mathf.PingPong(Time.time * breathSpeed, maxIntensity - minIntensity) + minIntensity;

        // 將目標強度應用到光源上
        breathingLight.intensity = targetIntensity;
    }
}
