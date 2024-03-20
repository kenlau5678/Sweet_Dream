using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float hitSpeed; // 受击后退速度
    public float hitDuration = 0.2f; // 受击后退持续时间
    public bool isHit; // 受击判定
    private Vector2 hitDirection; // 击退方向
    private Animator animator;
    new private Rigidbody2D rigidbody;

    void Start()
    {
        //animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // PlayerHit 类的部分修改
    public void GetHit(Vector2 hitDirection)
    {
        if (!isHit) // 确保角色不会在受击状态时再次受击
        {
            // 更新角色朝向
            float newScaleX = hitDirection.x < 0 ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(-newScaleX, transform.localScale.y, transform.localScale.z);

            this.hitDirection = hitDirection.normalized;
            StartCoroutine(HitBack());
        }
    }

    private IEnumerator HitBack()
    {
        isHit = true;
        
        rigidbody.velocity = hitDirection * hitSpeed;

        yield return new WaitForSeconds(hitDuration); // 等待一段时间

        
        isHit = false; // 重置受击状态
    }
}
