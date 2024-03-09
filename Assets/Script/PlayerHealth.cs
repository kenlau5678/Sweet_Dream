using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public static class DOTweenExtensions
{
    public static TweenerCore<Color, Color, ColorOptions> DOColor(this Light2D light, Color endValue, float duration)
    {
        return DOTween.To(() => light.color, x => light.color = x, endValue, duration);
    }
}

public class PlayerHealth : MonoBehaviour
{

    //public HealthBar healthBar;

    public int maxHeath = 100;
    public int currentHeath;
    //public Animator animator;
    public float intensity;
    public float shaketime;
    public Rigidbody2D rb;
    public float force;
    public Light2D light;
    public Animator animator;
    void Start()
    {
        currentHeath = maxHeath;
        
        //healthBar.SetMaxhealth(maxHeath);
    }

    public void TakeDamage(int damge)
    {
        currentHeath -= damge;
        //healthBar.SetHealth(currentHeath);
        Debug.Log(currentHeath);
        //animator.SetTrigger("Hurt");
        //GameManager.camShake.Shake();
        CameraShake.Instance.shakeCamera(intensity, shaketime);
        light.DOColor(new Color(100f/255f, 100f / 255f, 100f / 255f), 0.1f).OnComplete(() => light.DOColor(Color.white, 1f));
        if (currentHeath <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("player died");
        CameraShake.Instance.shakeCamera(2*intensity, shaketime);
        animator.SetTrigger("Die");
        
        light.DOColor(new Color(25f / 255f, 25f / 255f, 25f / 255f), 0.2f).OnComplete(() => light.DOColor(Color.white, 1f));
        
       // gameObject.SetActive(false);
        //GetComponent<Collider2D>().enabled = false; 
        Invoke("Reborn", 0.75f);
    }

    public void Reborn()
    {
        transform.position = GetComponent<PlayerMovement>().SavePos;
       // gameObject.SetActive(true);
    }

    //public void heal(int health)
    //{
    //    currentHeath += health;
        
        
    //    if(currentHeath>=maxHeath)
    //    {
    //        currentHeath = maxHeath;
    //    }
    //    healthBar.SetHealth(currentHeath);
    //    Debug.Log(currentHeath);
    //}

}