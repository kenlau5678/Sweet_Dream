using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHeath = 100;
    int currentHeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHeath = maxHeath;

    }

    public void TakeDamage(int damge)
    {
        currentHeath -= damge;
        if (currentHeath <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        Debug.Log("enemy died");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
