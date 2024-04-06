using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public static class DOTweenExtensions
{
    public static TweenerCore<Color, Color, ColorOptions> DOColor(this Light2D light, Color endValue, float duration)
    {
        return DOTween.To(() => light.color, x => light.color = x, endValue, duration);
    }
}

public class PlayerHealth : MonoBehaviour
{

    public HealthBar healthBar;

    public int maxHeath = 100;
    public int currentHeath;
    //public Animator animator;
    public float intensity;
    public float shaketime;
    public Rigidbody2D rb;
    public float force;
    public Light2D light;
    public Animator animator;
    public ParticleSystem Blood;
    public Image BG;
    void Start()
    {
        currentHeath = maxHeath;
        if(healthBar!=null)
        {
            healthBar.SetMaxhealth(maxHeath);
        }
        
    }

    public void HPHealth(int health)
    {
        currentHeath += health;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHeath);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHeath -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHeath);
        }
        if (Blood != null) Blood.Play();
        CameraShake.Instance.shakeCamera(intensity, shaketime);
        this.GetComponent<TimeStop>().StopTime(0.1f, 10, 0.1f);
        // 应用颜色变化
        var darkColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);
        light.DOColor(darkColor, 0.1f).OnComplete(() => light.DOColor(Color.white, 1f));
        BG.DOColor(darkColor, 0.1f).OnComplete(() => BG.DOColor(Color.white, 1f)); // 同时变化BG颜色

        if (currentHeath <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        var deathColor = new Color(25f / 255f, 25f / 255f, 25f / 255f);
        light.DOColor(deathColor, 0.2f).OnComplete(() => light.DOColor(Color.white, 1f));
        BG.DOColor(deathColor, 0.2f).OnComplete(() => BG.DOColor(Color.white, 1f)); // 同时变化BG颜色
        CameraShake.Instance.shakeCamera(3 * intensity, shaketime);
        // gameObject.SetActive(false);
        //GetComponent<Collider2D>().enabled = false; 
        Invoke("Reborn", 0.75f);
    }

    public void Reborn()
    {
        transform.position = GetComponent<PlayerMovement>().SavePos;
        currentHeath = maxHeath;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHeath);
        }
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