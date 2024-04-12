using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BlueBoxWall : MonoBehaviour
{
    public GameObject fadeObject;
    public float fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {

            collision.gameObject.transform.SetParent(this.transform);
            if(fadeObject != null)
            {
                Image fadeImage = fadeObject.GetComponent<Image>();

                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
                fadeImage.DOFade(0, fadeTime);
            }
           
            this.gameObject.SetActive(false);

        }
    }
}
