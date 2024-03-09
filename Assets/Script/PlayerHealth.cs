using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    //public HealthBar healthBar;

    public int maxHeath = 100;
    public int currentHeath;
    //public Animator animator;
    public float intensity;
    public float shaketime;

    void Start()
    {
        //currentHeath = maxHeath;
        //healthBar.SetMaxhealth(maxHeath);
    }

    //public void TakeDamage(int damge)
    //{
    //    currentHeath -= damge;
    //    healthBar.SetHealth(currentHeath);
    //    Debug.Log(currentHeath);
    //    //animator.SetTrigger("Hurt");
    //    //GameManager.camShake.Shake();
    //    CameraShake.Instance.shakeCamera(intensity, shaketime);
    //    if (currentHeath <= 0)
    //    {
    //        Die();
    //    }
    //}

    public void Die()
    {
        Debug.Log("player died");
        CameraShake.Instance.shakeCamera(intensity, shaketime);
        //animator.SetBool("IsDead", true);
       // gameObject.SetActive(false);
        //GetComponent<Collider2D>().enabled = false; 
        Invoke("Reborn", 0.5f);
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