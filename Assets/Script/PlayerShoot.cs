using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletforce = 20f;
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

        Destroy(bullet, 1f);
    }

        
    
}
