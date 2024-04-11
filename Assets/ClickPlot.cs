using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickPlot : MonoBehaviour
{
    public List<GameObject> itemsToShow; // �惦Ҫ�@ʾ���Ŀ
    private int currentIndex = 0;
    private bool isCoolingDown = false;
    public float coolDown;
    public GameObject fadeOut;
    public string startSceneName;

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

        else if (currentIndex == itemsToShow.Count)
        {
            if (fadeOut != null)
            {
                fadeOut.GetComponent<Fade>().FadeOut();
                currentIndex++;
            }
        }
        else
        {
            SceneManager.LoadScene(startSceneName);
        }
    }

}
