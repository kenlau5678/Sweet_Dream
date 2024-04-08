using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{   public Transform player;
	public float maxHeath = 100f;
    public float currentHeath;

    public float attackRange;
    public float distanceToPlayer;
	public bool isFlipped = false;


	private AnimatorStateInfo info;//动画状态
	private Animator animator;
	new private Rigidbody2D rigidbody;


	void Start()
	{
		animator = transform.GetComponent<Animator>();
		rigidbody = transform.GetComponent<Rigidbody2D>();

		currentHeath = maxHeath;
	}

	void Update()
	{
		DistanceToPlayer();
		//LookAtPlayer();
		info = animator.GetCurrentAnimatorStateInfo(0);

         if (Input.GetKeyDown(KeyCode.J)) //模拟受伤
        {
           
            TakeDamage(1);
            
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

	public void DistanceToPlayer() // 获取Boss和玩家之间的距离
	{
		
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
		if(distanceToPlayer <= attackRange)
		{
			Debug.Log(distanceToPlayer);
		}
		
	}
	public void TakeDamage(float damage) //受到伤害计算
	{
		currentHeath -= damage;
		Debug.Log("Boss2 gets hit");
        Debug.Log("Boss2 受到 " + damage + " 点伤害，剩余生命值：" + currentHeath);
		animator.SetTrigger("Hit");
		if(currentHeath <= 0)
		{
			Die();
		}
	}
	public void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity); //deathAnimator
		
		Destroy(gameObject);
	}
}
