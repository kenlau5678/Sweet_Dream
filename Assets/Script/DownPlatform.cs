using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;
    private Rigidbody2D rb;
    public float shakeDuration = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlayerOnPlatform)
        {
            StartCoroutine(ShakeAndDrop());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isPlayerOnPlatform = false;
        }
    }

    IEnumerator ShakeAndDrop()
    {
        // 记录原始平台位置
        Vector3 originalPosition = transform.position;

        // 抖动参数
        float shakeMagnitude = 0.1f;
        
        float dropSpeed = 2f;

        // 抖动
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 恢复原始位置
        transform.position = originalPosition;

        // 下落
        rb.isKinematic = false;
        rb.gravityScale = dropSpeed;
    }
}
