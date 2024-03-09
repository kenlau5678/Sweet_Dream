using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;
	public float health = 100f;

	public bool isFlipped = false;

	public float deathExplosionRadius = 10f;


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
		if(health <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity); //deathAnimator
		Collider[] colliders = Physics.OverlapSphere(transform.position, deathExplosionRadius);// 在敌人死亡位置周围创建一个伤害范围
        foreach (Collider col in colliders)// 对范围内的所有游戏对象应用伤害
        {
            // 检查是否是可以受到伤害的对象
            if (col.CompareTag("Player") || col.CompareTag("Monster"))
            {
                //col.GetComponent<Health>().TakeDamage(damageAmount);
            }
        }

		Destroy(gameObject);
	}
}