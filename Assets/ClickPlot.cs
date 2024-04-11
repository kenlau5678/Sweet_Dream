using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlot : MonoBehaviour
{
    public List<GameObject> itemsToShow; // �惦Ҫ�@ʾ���Ŀ
    private int currentIndex = 0;
    private bool isCoolingDown = false;
    public float coolDown;
    private void Update()
    {
        // �z���Ƿ����c��ݔ���Ҳ�����s��
        if (Input.GetMouseButtonDown(0) && !isCoolingDown)
        {
            StartCoroutine(PlayItemWithCooldown());
        }
    }

    // �����Ŀ�K�����s�r�g
    private IEnumerator PlayItemWithCooldown()
    {
        isCoolingDown = true;
        ShowNextItem(); // �����Ŀ

        yield return new WaitForSeconds(coolDown); // ��s�r�g

        isCoolingDown = false; // ��s�Y��
    }

    // �@ʾ��һ���Ŀ
    private void ShowNextItem()
    {
        if (currentIndex < itemsToShow.Count)
        {
            itemsToShow[currentIndex].SetActive(true); // �@ʾ��ǰ����̎���Ŀ
            currentIndex++;
        }
    }
}
