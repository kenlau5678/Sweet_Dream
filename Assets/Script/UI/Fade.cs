using DG.Tweening;
using System;
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
    public Text fadeText; // Add a reference to the text component
    public enum INOUT
    {
        fadein,
        fadeout
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = fadeObject.GetComponent<Image>();
        fadeImage.enabled = true;

        // Get the text component if available
        fadeText = fadeObject.GetComponentInChildren<Text>();

        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        if (fade == INOUT.fadein)
        {
            DOTween.To(() => fadeImage.color, x => fadeImage.color = x, new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0), fadetime);
            if (fadeText != null)
            {
                DOTween.To(() => fadeText.color, x => fadeText.color = x, new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 0), fadetime);
            }
        }
    }

    public void FadeOut()
    {
        if (fade == INOUT.fadeout)
        {
            DOTween.To(() => fadeImage.color, x => fadeImage.color = x, new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1), fadetime);
            if (fadeText != null)
            {
                DOTween.To(() => fadeText.color, x => fadeText.color = x, new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 1), fadetime);
            }
        }
    }
}
