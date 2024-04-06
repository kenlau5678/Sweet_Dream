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
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (GetComponent<MagicPower>().currentMagicPower < 20) return;

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

        if (GetComponent<MagicPower>().currentMagicPower < 20)
        {
            GetComponent<MagicPower>().currentMagicPower = 0;
        }
        else
        {
            GetComponent<MagicPower>().currentMagicPower -= 20;
        }

        GetComponent<MagicPower>().magicBar.SetMagic(GetComponent<MagicPower>().currentMagicPower);
        Destroy(bullet, 1f);
    }

        
    
}
