using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletforce = 2f;
    public Animator animator;
    GameObject bullet;
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
        if (bullet != null) { Destroy(bullet); }
        animator.SetTrigger("Shoot");
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (transform.lossyScale.x > 0)
        {
            rb.AddForce(firePoint.right * bulletforce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(firePoint.right * bulletforce * -1f, ForceMode2D.Impulse);
        }

        //�����и�bug?���ӵ�����������������һ���ӵ���Ҳ��ը����Ϊѡ��bulletΪ��ͨ�ĸ���type��

       
    }
}
