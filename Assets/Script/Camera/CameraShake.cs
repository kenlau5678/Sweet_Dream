// 使用Cinemachine库
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个CameraShake类，用于实现相机抖动效果
public class CameraShake : MonoBehaviour
{
    // 单例模式，方便在其他脚本中直接访问
    public static CameraShake Instance { get; private set; }

    // Cinemachine虚拟相机组件
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    // 抖动持续时间计时器
    private float shakeTimer;

    // 在Awake阶段，初始化单例和获取Cinemachine虚拟相机组件
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // 在Update中更新抖动效果的逻辑
    private void Update()
    {
        // 如果抖动计时器大于0，表示抖动正在进行中
        if (shakeTimer > 0)
        {
            // 减少抖动计时器，模拟抖动逐渐减弱
            shakeTimer -= Time.deltaTime;

            // 如果计时器到0或以下，抖动效果结束，重置振幅为0，停止抖动
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    // 提供一个公共方法来启动相机抖动效果，需要指定抖动强度和持续时间
    public void shakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    // 另一个公共方法，允许在启动相机抖动时指定频率和强度以及时间
    public void shakeCameraWithFrequency(float intensity, float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        shakeTimer = time;
    }
}
