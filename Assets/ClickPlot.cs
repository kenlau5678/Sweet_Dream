using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlot : MonoBehaviour
{
    public List<GameObject> itemsToShow; // 存儲要顯示的項目
    private int currentIndex = 0;
    private bool isCoolingDown = false;
    public float coolDown;
    private void Update()
    {
        // 檢查是否有點擊輸入且不在冷卻中
        if (Input.GetMouseButtonDown(0) && !isCoolingDown)
        {
            StartCoroutine(PlayItemWithCooldown());
        }
    }

    // 播放項目並添加冷卻時間
    private IEnumerator PlayItemWithCooldown()
    {
        isCoolingDown = true;
        ShowNextItem(); // 播放項目

        yield return new WaitForSeconds(coolDown); // 冷卻時間

        isCoolingDown = false; // 冷卻結束
    }

    // 顯示下一個項目
    private void ShowNextItem()
    {
        if (currentIndex < itemsToShow.Count)
        {
            itemsToShow[currentIndex].SetActive(true); // 顯示當前索引處的項目
            currentIndex++;
        }
    }
}
