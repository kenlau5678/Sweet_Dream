using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isTriggered = false;
    public GameObject Level;
    public GameObject NextLevel;
    public GameObject Background;
    public GameObject NextBackground;
    void Update()
    {
        // 检测是否在触发状态且按下了J键
        if (isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            ReactToTrigger();
            Level.SetActive(false);
            Background.SetActive(false);
            NextLevel.SetActive(true);
            NextBackground.SetActive(true);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTriggered = false;
        }
    }

    void ReactToTrigger()
    {
        // 在这里编写你的反应代码，例如：
        Debug.Log("Reacting to trigger after pressing J!");
    }
}