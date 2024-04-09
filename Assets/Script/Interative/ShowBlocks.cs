using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBlocks : MonoBehaviour
{
    public GameObject[] blocks; // �� Unity �༭����ָ����Щ GameObjects
    public bool isTrigger;
    public float fadeDuration = 1f; // ��ʧ�ĳ���ʱ��
    public GameObject keyUI;
    // Start is called before the first frame update
    void Start()
    {
        // ��ʼʱ�������е� blocks
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
        // ����ҽӴ������� E ��ʱ
        if (collision.tag == "Player" )
        {
            // ��ʾ���е� blocks
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
