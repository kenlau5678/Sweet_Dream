using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> platforms;

    void Start()
    {
        // ��ȡ���б��Ϊ "Platform" �� GameObject
        platforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Platform"));
        setFalseActive();
    }

    // ���� PlatformChangeAppearing ����
    public void TriggerPlatformChangeAppearing()
    {
        foreach (var platform in platforms)
        {
            // ���� PlatformChangeAppearing ��ƽ̨�ϵ�����е�һ������
            platform.GetComponent<IntervalsAppearingPlatform>().ChangeAppearing();
        }
    }

    public void setFalseActive()
    {
        foreach (var platform in platforms)
        {
            // ���� PlatformChangeAppearing ��ƽ̨�ϵ�����е�һ������
            if (!platform.GetComponent<IntervalsAppearingPlatform>().flag)
            {
                platform.SetActive(false);
            };
        }
    }
}
