using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;
    public GameObject hitEffect;
    public float intensity;
    public float shaketime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);

            if (collision.collider.tag == "Monster")
            {
                Debug.Log("monster");
                CameraShake.Instance.shakeCamera(intensity, shaketime);
                collision.collider.GetComponent<Monster>().TakeDamage(damage);
                //collision.collider.GetComponent<Boss>().TakeDamage(damage);
            }
            else if(collision.collider.tag == "Boss")
            {
                Debug.Log("Boss");
                CameraShake.Instance.shakeCamera(intensity, shaketime);
                collision.collider.GetComponent<BossHeart>().TakeDamage(damage);
            }
            else if (collision.collider.tag == "Boss1")
            {
                collision.collider.GetComponent<Boss>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
    
}
