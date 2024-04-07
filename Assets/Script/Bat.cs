using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float radius = 5.0f; // 移动半径
    public float moveSpeed = 2.0f; // 移动速度

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isFacingRight = false; // 初始面向

    void Start()
    {
        startPosition = transform.position; // 保存初始位置
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

    // 根据移动方向改变对象的面向
    void FlipDirection()
    {
        if ((targetPosition.x > transform.position.x && !isFacingRight) ||
            (targetPosition.x < transform.position.x && isFacingRight))
        {
            // 改变面向
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    // 在Unity编辑器中绘制Gizmos
    private void OnDrawGizmos()
    {
        if (startPosition == Vector2.zero)
        {
            startPosition = transform.position;
        }

        // 绘制表示移动范围的圆
        Gizmos.color = Color.blue; // 设置Gizmos颜色为蓝色
        Gizmos.DrawWireSphere(startPosition, radius); // 以对象的初始位置为中心，绘制半径为radius的线框圆圈
    }
}
