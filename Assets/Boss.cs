using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
	public float maxHeath = 100f;
    public float currentHeath;

	public bool isFlipped = false;

	public float hitSpeed;//受击后退速度
	private bool isHit;//受击判定
	private Vector2 hitDirection;//击退方向
	private AnimatorStateInfo info;//动画状态
	private Animator animator;
	new private Rigidbody2D rigidbody;
	private int attackCount = 0;

	void Start()
	{
		animator = transform.GetComponent<Animator>();
		rigidbody = transform.GetComponent<Rigidbody2D>();

		currentHeath = maxHeath;
	}

	void Update()
	{
		LookAtPlayer();
		info = animator.GetCurrentAnimatorStateInfo(0);
		if(isHit)
		{
			rigidbody.velocity = hitDirection * hitSpeed;
			if(info.normalizedTime >= 0.6f)
			{ isHit = false;}
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

	public void GetHit(Vector2 hitDirection)
	{
		//transform.localScale = new Vector3(-hitDirection.x,1,1);//受击朝向伤害来源
		isHit = true;
		this.hitDirection = hitDirection;
		animator.SetTrigger("Hit");
	}
	public void TakeDamage(float damage)
	{
		currentHeath -= damage;
		Debug.Log("monster gets hit");
		animator.SetTrigger("Hit");
		if(currentHeath <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity); //deathAnimator
		
		Destroy(gameObject);
	}
}
