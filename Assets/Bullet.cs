using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D  rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed; //Bullet's velocity setting
    }
    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        Monster monster = hitInfo.GetComponent<Monster>();
        Debug.Log(hitInfo.name);
        if(monster != null)
        {
            monster.TakeDamage(damage);
        }
        //Destroy(gameObject);/*这里有bug，MissingReferenceException: The object of type 'GameObject' has been destroyed but you are still trying to access it. Your script should either check if it is null or you should not destroy the object*/
        
       
    }
    // Update is called once per frame
   
}
