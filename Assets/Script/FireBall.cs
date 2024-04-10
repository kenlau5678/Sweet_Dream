using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float Xspeed;
    private FireBallManager fireBallManager;
    // Start is called before the first frame update
    void Start()
    {
        fireBallManager = transform.parent.GetComponent<FireBallManager>();
        Xspeed = Random.Range(0,2);
        if(Xspeed==0) Xspeed = -5;//斜着的雨
        else  Xspeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Xspeed*Time.deltaTime,-moveSpeed*Time.deltaTime,0);
        if (transform.position.y < -11.5f)
        {
            fireBallManager.SpawnFireBall();
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            fireBallManager.SpawnFireBall();
            Destroy(gameObject);
        }
    }
}
