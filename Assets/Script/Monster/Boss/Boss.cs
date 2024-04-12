using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public HealthBar healthBar;
    public Transform player;
	public int maxHeath = 250;
    public int health;

	public bool isFlipped = false;
	public float attackRange = 5f;


	private bool isDead = false;
	private AnimatorStateInfo info;//动画状态
	private Animator animator;
	new private Rigidbody2D rb;

	//Effect 
	public float knockbackForce = 5f;
    public float intensity;
    public float shaketime;

	public ParticleSystem Blood;

	public GameObject showObject;
	void Start()
	{
		animator = transform.GetComponent<Animator>();
		rb = transform.GetComponent<Rigidbody2D>();

		health = maxHeath;
		if (healthBar!=null)
		{
            
            healthBar.SetMaxhealth(maxHeath);
        }
		showObject.SetActive(false);
	}

	void Update()
	{
		DistanceToPlayer();
		LookAtPlayer();
		info = animator.GetCurrentAnimatorStateInfo(0);
		

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
		
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
		if(distanceToPlayer <= attackRange)
		{
			Debug.Log(distanceToPlayer);
		}
		
	}
	
	public void TakeDamage(int damage)  //受伤代码
	{
		if (isDead) return; // 如果Boss已经死亡，直接返回，不再执行下面的代码
		health -= damage;
		if (healthBar != null)
		{ healthBar.SetHealth(health); }
		AudioManager.instance.PlaySFX("Hit");
		CameraShake.Instance.shakeCamera(intensity, shaketime);
		Debug.Log("Boss gets hit, hp:"+health);
		animator.SetTrigger("Hit"); //Boss受击动画
		if(Blood!= null)
		{
			Blood.Play();
		}
		if (transform.position.x - player.transform.position.x > 0)
		{

			rb.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
		}
		else
		{
			rb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);

		}
		if(health <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity); //deathAnimator
		if (isDead) return; // 防止重复调用
        isDead = true;
		animator.SetTrigger("Die");
		Invoke("DestroyObject", 5f);
	}
	private void DestroyObject()
	{
		Destroy(gameObject);
		showObject.SetActive(true);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // ���������˺�
            collision.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);

            // �������
            PlayerHit playerhit = collision.collider.GetComponent<PlayerHit>();
            if (playerhit != null)
            {
                playerhit.GetComponent<PlayerMovement>().isHit = true;
                if (transform.position.x - playerhit.transform.position.x > 0)
                {

                    rb.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
                    playerhit.GetHit(playerhit.transform.position - transform.position);
                }
                else
                {
                    rb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
                    playerhit.GetHit(playerhit.transform.position - transform.position);
                }
            }
        }
    }
}
