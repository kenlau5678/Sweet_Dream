using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float fadetime = 1f;
    public GameObject fadeObject;
    public INOUT fade;
    public Image fadeImage;
    public enum INOUT
    {
        fadein,
        fadeout
    }
    // Start is called before the first frame update
    void Start()
    {

        FadeIn();
        fadeImage = fadeObject.GetComponent<Image>();
        fadeImage.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        if (fade == INOUT.fadein)
        {
            fadeObject.GetComponent<Image>().DOFade(0, fadetime);
        }
    }

    public void FadeOut()
    {
        if (fade == INOUT.fadeout)
        {
            fadeImage.DOFade(1f, fadetime);
        }
    }
}
