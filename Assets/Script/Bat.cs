using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float radius = 5.0f; // �ƶ��뾶
    public float moveSpeed = 2.0f; // �ƶ��ٶ�

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isFacingRight = false; // ��ʼ����

    void Start()
    {
        startPosition = transform.position; // �����ʼλ��
        SetRandomTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();
        FlipDirection();
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        targetPosition = startPosition + randomPoint;
    }

    // �����ƶ�����ı���������
    void FlipDirection()
    {
        if ((targetPosition.x > transform.position.x && !isFacingRight) ||
            (targetPosition.x < transform.position.x && isFacingRight))
        {
            // �ı�����
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    // ��Unity�༭���л���Gizmos
    private void OnDrawGizmos()
    {
        if (startPosition == Vector2.zero)
        {
            startPosition = transform.position;
        }

        // ���Ʊ�ʾ�ƶ���Χ��Բ
        Gizmos.color = Color.blue; // ����Gizmos��ɫΪ��ɫ
        Gizmos.DrawWireSphere(startPosition, radius); // �Զ���ĳ�ʼλ��Ϊ���ģ����ư뾶Ϊradius���߿�ԲȦ
    }
}
