using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBlocks : MonoBehaviour
{
    public GameObject[] blocks; // 在 Unity 编辑器中指定这些 GameObjects
    public bool isTrigger;
    public float fadeDuration = 1f; // 消失的持续时间
    public GameObject keyUI;
    // Start is called before the first frame update
    void Start()
    {
        // 初始时隐藏所有的 blocks
        foreach (GameObject block in blocks)
        {

            block.GetComponent<SpriteRenderer>().DOFade(0f, fadeDuration);
            block.SetActive(false);


        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if(keyUI)
            {
                keyUI.SetActive(false);
            }
            foreach (GameObject block in blocks)
            {
                
                block.SetActive(true);
                block.GetComponent<SpriteRenderer>().DOFade(1f, fadeDuration);
            }
            
            this.GetComponent<SpriteRenderer>().DOFade(0f,fadeDuration).OnComplete(() => gameObject.SetActive(false)); ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当玩家接触并按下 E 键时
        if (collision.tag == "Player" )
        {
            // 显示所有的 blocks
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isTrigger = false;
        }
    }
}
