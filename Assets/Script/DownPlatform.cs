using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;
    private Rigidbody2D rb;
    public float shakeDuration = 1f;
    public float resetDelay = 2f; // ƽ̨�ص�ԭλǰ�ĵȴ�ʱ��
    private Vector3 originalPosition; // ��¼ƽ̨��ԭʼλ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position; // �� Start �м�¼ԭʼλ��
    }

    void Update()
    {
        if (isPlayerOnPlatform)
        {
            StartCoroutine(ShakeAndDrop());
            isPlayerOnPlatform = false; // ��ֹЭ���ظ�����
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
        // ��������
        float shakeMagnitude = 0.1f;
        float dropSpeed = 2f;

        // ����
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ָ�ԭʼλ��
        transform.position = originalPosition;

        // ����
        rb.isKinematic = false;
        rb.gravityScale = dropSpeed;

        // �ȴ�һ��ʱ��
        yield return new WaitForSeconds(resetDelay);

        // �ص�ԭλ
        rb.velocity = Vector2.zero; // ֹͣ�˶�
        rb.gravityScale = 0f;
        rb.isKinematic = true;
        transform.position = originalPosition;
    }
}
