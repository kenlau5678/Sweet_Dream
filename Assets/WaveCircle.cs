using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCircle : MonoBehaviour
{
    public float waveDamage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Wave hit Player");
        }
    }
}
