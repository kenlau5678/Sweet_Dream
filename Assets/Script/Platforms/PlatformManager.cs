using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> platforms;

    void Start()
    {
        // 获取所有标记为 "Platform" 的 GameObject
        platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Platform"));
        setFalseActive();
    }

    // 触发 PlatformChangeAppearing 函数
    public void TriggerPlatformChangeAppearing()
    {
        foreach (var platform in platforms)
        {
            // 假设 PlatformChangeAppearing 是平台上的组件中的一个函数
            platform.GetComponent<IntervalsAppearingPlatform>().ChangeAppearing();
        }
    }

    public void setFalseActive()
    {
        foreach (var platform in platforms)
        {
            // 假设 PlatformChangeAppearing 是平台上的组件中的一个函数
            if (!platform.GetComponent<IntervalsAppearingPlatform>().flag)
            {
                platform.SetActive(false);
            };
        }
    }
}
