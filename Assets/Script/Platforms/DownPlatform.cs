using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;
    private Rigidbody2D rb;
    public float shakeDuration = 1f;
    public float resetDelay = 2f; // 平台回到原位前的等待时间
    private Vector3 originalPosition; // 记录平台的原始位置

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position; // 在 Start 中记录原始位置
    }

    void Update()
    {
        if (isPlayerOnPlatform)
        {
            StartCoroutine(ShakeAndDrop());
            isPlayerOnPlatform = false; // 防止协程重复启动
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

        // 等待一段时间
        yield return new WaitForSeconds(resetDelay);

        // 回到原位
        rb.velocity = Vector2.zero; // 停止运动
        rb.gravityScale = 0f;
        rb.isKinematic = true;
        transform.position = originalPosition;
    }
}
