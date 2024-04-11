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
        
        if(GetComponent<PlayerPosition>().level == 3)
        {
            currentHeath-=15;
            healthBar.SetHealth(currentHeath);
        }
    }

    public void HPHealth(int health)
    {
        currentHeath += health;
        if(currentHeath > maxHeath)
        { 
            currentHeath = maxHeath; 
        }
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHeath);
        }
        AudioManager.instance.PlaySFX("Heal");
    }

    public void TakeDamage(int damage)
    {
        //��Ѫ
        currentHeath -= damage;
        //Ѫ��UI�ı�
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHeath);
        }
        //����������Ч
        if (Blood != null) Blood.Play();
        //��ͷ����
        CameraShake.Instance.shakeCamera(intensity, shaketime);
        //��֡ʱͣЧ��
        this.GetComponent<TimeStop>().StopTime(0.1f, 10, 0.1f);
        //������Ч��
        var darkColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);
        light.DOColor(darkColor, 0.1f).OnComplete(() => light.DOColor(Color.white, 1f));
        BG.DOColor(darkColor, 0.1f).OnComplete(() => BG.DOColor(Color.white, 1f)); // ͬʱ�仯BG��ɫ
        //�ж��Ƿ�����
        if (currentHeath <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        var deathColor = new Color(25f / 255f, 25f / 255f, 25f / 255f);
        light.DOColor(deathColor, 0.2f).OnComplete(() => light.DOColor(Color.white, 1f));
        BG.DOColor(deathColor, 0.2f).OnComplete(() => BG.DOColor(Color.white, 1f)); // ͬʱ�仯BG��ɫ
        AudioManager.instance.PlaySFX("Hit");
        CameraShake.Instance.shakeCamera(3 * intensity, shaketime);
        // gameObject.SetActive(false);
        //GetComponent<Collider2D>().enabled = false; 
        animator.SetTrigger("Die");
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