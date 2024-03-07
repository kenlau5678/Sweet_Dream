using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
	public float health = 100f;

	public bool isFlipped = false;

	//private bool isHit;
	private Vector2 direction;
	private AnimatorStateInfo info;
	private Animator animator;

	private int attackCount = 0;
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

	public void TakeDamage(int damage)
	{
		health -= damage;
		animator.SetTrigger("Hit");
		if(health <= 0)
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
