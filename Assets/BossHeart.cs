using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class BossHeart : MonoBehaviour
{
    public int MaxHP;
    public int currentHP;
    public HealthBar healthBar;
    public float intensity = 5f;
    public float shaketime = 0.2f;
    public Animator animator;
    public ParticleSystem Blood;
    public GameObject LoadObject;
    void Start()
    {
        LoadObject.SetActive(false);
        currentHP = MaxHP;
        if (healthBar != null)
        {

            healthBar.SetMaxhealth(MaxHP);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (healthBar != null)
        { 
            healthBar.SetHealth(currentHP); 
        }
        AudioManager.instance.PlaySFX("Hit");
        CameraShake.Instance.shakeCamera(intensity, shaketime);
        animator.SetTrigger("Hit");
        Blood.Play();
        if (currentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("boss died");
        Destroy(gameObject);
        GetComponent<Collider2D>().enabled = false;
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false);
        }
        LoadObject.SetActive(true);
    }


}
