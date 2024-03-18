using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float maxHeath = 100f;
    float currentHeath;
    public DetectionZone detectionZone;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Transform player;
    public Collider2D col;
    public bool isFlipped = false;
    public float knockbackForce = 5f;
    // Ѳ����ر���
    public Transform[] patrolPoints; // Ѳ�ߵ�����
    private int currentPatrolPointIndex = 0; // ��ǰѲ�ߵ�����
    // Start is called before the first frame update
    void Start()
    {
        currentHeath = maxHeath;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
       
        if (detectionZone.detectedObjs.Count > 0)
        {
            LookAtPlayer();
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed);
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Transform targetPatrolPoint = patrolPoints[currentPatrolPointIndex];
        float directionX = Mathf.Sign(targetPatrolPoint.position.x - transform.position.x);
        rb.AddForce(new Vector2(directionX * moveSpeed, 0));

        // ����Ƿ񵽴ﵱǰѲ�ߵ�� X λ��
        if (Mathf.Abs(transform.position.x - targetPatrolPoint.position.x) < 0.1f)
        {
            // �л�����һ��Ѳ�ߵ�
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        // ���������������ƶ�����
        Vector3 scale = transform.localScale;
        if (directionX < 0 && isFlipped || directionX > 0 && !isFlipped)
        {
            isFlipped = !isFlipped;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }



    public void LookAtPlayer()
    {

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // ���������˺�
            collision.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);

            // �������
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.GetComponent<PlayerMovement>().isHit = true;
                if(transform.position.x - playerRb.transform.position.x>0)
                {
                   
                   rb.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
                   playerRb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
                    playerRb.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
                }
                playerRb.GetComponent<PlayerMovement>().isHit = false;
            }
        }
    }
}
