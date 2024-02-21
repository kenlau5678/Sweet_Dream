using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
                CameraShake.Instance.shakeCamera(intensity, shaketime);
                //collision.collider.GetComponent<Monster>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
