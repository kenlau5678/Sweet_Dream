using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarForBoss2 : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Laser Bar hit player");
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
