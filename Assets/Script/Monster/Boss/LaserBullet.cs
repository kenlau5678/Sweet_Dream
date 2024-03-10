using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField]public float speed = 20f;
    public int laserDamage = 10;
    public Rigidbody2D rb;
    public float intensity;
    public float shaketime;
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();//获取子弹刚体组件
        rb.velocity = transform.right * -speed;//移动
        //BUG:Destroy(gameObject, 2f);//2秒后销毁子弹，不然子弹会无限多
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Shooted");
            if(other.CompareTag("Player"))
            {
                Debug.Log("shootPlayer");
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(laserDamage);
                }
            }
            //Destroy(gameObject);
        
    }
}
