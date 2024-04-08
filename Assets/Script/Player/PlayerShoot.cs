using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletforce = 2f;
    public Animator animator;
    private MagicPower MP;
    float nextShootTime = 0f;
    public float ShootRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = GetComponent<PlayerPosition>().level;
        switch (currentLevel)
        {
            case 1:
            case 2:
                // ����Ǽ���1��2����ִ���κβ�����
                break;
            default:
                // ���������м��𣬻�ȡMagicPower�����
                MP = GetComponent<MagicPower>();
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
                nextShootTime = Time.time + 1f /ShootRate;
            }
        }
    }

    public void Shoot()
    {
        if(MP != null)
        {
            if (MP.currentMagicPower < 20) return;
        }


        animator.SetTrigger("Shoot");
        AudioManager.instance.PlaySFX("Shoot");
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
        if (MP != null)
        {
            if (MP.currentMagicPower < 20)
            {
                MP.currentMagicPower = 0;
            }
            else
            {
                MP.currentMagicPower -= 20;
            }

            MP.magicBar.SetMagic(MP.currentMagicPower);
        }

        
        Destroy(bullet, 1f);
    }

        
    
}
