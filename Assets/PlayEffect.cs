using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayEffect : MonoBehaviour
{
    public ParticleSystem triggerEffect;
    public bool canPlay = false;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            triggerEffect.Play(); // 直接在这里播放效果
        }
       
    }

}
