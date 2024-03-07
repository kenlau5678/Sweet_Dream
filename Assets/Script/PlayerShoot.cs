using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletforce = 2f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (transform.lossyScale.x > 0)
                {
                    rb.AddForce(firePoint.right * bulletforce, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(firePoint.right * bulletforce * -1f, ForceMode2D.Impulse);
                }

//这里有个bug?当子弹反弹回来，碰到下一颗子弹会也爆炸（因为选择bullet为普通的刚体type）

        Destroy(bullet, 1f);
    }

        
    
}
