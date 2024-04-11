using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCircle : MonoBehaviour
{
    public float waveDamage = 10f;
    public float changeTime;
    public float changSpeed;
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
            other.GetComponent<PlayerMovement>().ChangeSpeed(changSpeed, changeTime);
        }
    }
}
