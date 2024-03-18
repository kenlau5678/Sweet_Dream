using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SavePoint : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player" && collision.GetComponent<PlayerMovement>().SavePos != transform.position)
        {

            Image fadeImage = fadeObject.GetComponent<Image>();

            collision.GetComponent<PlayerMovement>().SavePos = transform.position;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
            fadeImage.DOFade(0, fadeTime);
            this.GetComponent<SaveGameData>().Save();
        }
    }
}
