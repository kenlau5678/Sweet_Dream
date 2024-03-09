using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("hit");
        if(other.CompareTag("Player")){
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
            PlayerHit playerHit = other.GetComponent<PlayerHit>();
            if(transform.localScale.x > 0)
                {
                    playerHit.GetHit(Vector2.right);
                }
            else if(transform.localScale.x < 0)
                {
                    playerHit.GetHit(Vector2.left);
                }
        }
    } 
}
