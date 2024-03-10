using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerHit : MonoBehaviour
{
    public float hitSpeed;//受击后退速度
	private bool isHit;//受击判定
	private Vector2 hitDirection;//击退方向
	private AnimatorStateInfo info;//动画状态
	private Animator animator;
	new private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
		rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHit(Vector2 hitDirection)
	{
		transform.localScale = new Vector3(hitDirection.x,1,1);//受击朝向伤害来源
		isHit = true;
		this.hitDirection = hitDirection;
		animator.SetTrigger("Hit");
	}
}


