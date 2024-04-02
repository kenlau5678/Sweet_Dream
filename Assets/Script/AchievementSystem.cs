using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Achievement
{
    public string achievementName;
    public string achievementDescription;
    public bool unlocked;
    public int count;
    public int Maxcount;
}

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; } // 单例访问点

    public List<Achievement> achievements = new List<Achievement>();

    public Transform achievementPanel;
    public Text achievementNameText;
    public Text achievementDescriptionText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 确保单例在场景加载时不会被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已存在实例，则销毁新对象
        }
    }

    private void Start()
    {
        //PopThePanel();
        // 初始化成就列表
        // 例如：achievements.Add(newVideoAmountAchievement);
        // 你可以在这里或在Inspector中添加成就

        // 注册事件
        // FindObjectOfType<UIManager>().UploadNewVideoAction += UploadNewVideo;
        // FindObjectOfType<UIManager>().GetNewSubscriberAction += GetNewSubscriber;
    }

    void UpdateAchievements(int countToAdd, string achievementType)
    {
        foreach (var ach in achievements)
        {
            if (ach.achievementName == achievementType && !ach.unlocked)
            {
                ach.count += countToAdd;
                if (ach.count >= ach.Maxcount)
                {
                    PopNewAchievement(ach);
                }
            }
        }
    }

    public void AttackAchieve()
    {
        UpdateAchievements(1, "ATTACK!"); // 假设 "New Video" 是你为视频数量成就设置的名称
        Debug.Log("ATTACK!");
    }
    public void JumpAchieve()
    {
        UpdateAchievements(1, "JUMP!"); // 假设 "New Video" 是你为视频数量成就设置的名称
        Debug.Log("JUMP!");
    }

    void PopNewAchievement(Achievement ach)
    {
        achievementNameText.text = ach.achievementName;
        achievementDescriptionText.text = ach.achievementDescription;

        ach.unlocked = true;


        PopThePanel();
    }

    void PopThePanel()
    {
        

        // 向左移动面板
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(achievementPanel.DOLocalMoveX(achievementPanel.localPosition.x-620, 1f).SetEase(Ease.OutExpo)); // 向左移动到0的位置，时间为1秒，使用OutExpo缓动函数
        mySequence.AppendInterval(1); // 保持1秒
        mySequence.Append(achievementPanel.DOLocalMoveX(achievementPanel.localPosition.x +620, 1f).SetEase(Ease.InExpo)); // 向右移动回初始位置，时间为1秒，使用InExpo缓动函数

        // 注意：根据你的面板起始位置和移动需求，你可能需要调整这些值
    }
}
