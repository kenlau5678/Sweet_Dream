using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public HealthBar healthBar;
    public Transform player;
	public int health;
	public int maxHeath = 250;

    public bool isFlipped = false;

	public float deathExplosionRadius = 10f;
	public Rigidbody2D rb;
	public float knockbackForce = 5f;
    public float intensity;
    public float shaketime;

	public ParticleSystem Blood;
    private void Start()
    {
        health = maxHeath;
        if (healthBar!=null)
		{
            
            healthBar.SetMaxhealth(maxHeath);
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

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (healthBar != null)
		{ healthBar.SetHealth(health); }
        AudioManager.instance.PlaySFX("Hit");
        CameraShake.Instance.shakeCamera(intensity, shaketime);
		Debug.Log("Takeed");
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
        if (health <= 0)
		{
			Die();
		}
	}
    void Die()
    {
        Debug.Log("enemy died");
        Destroy(gameObject);
        GetComponent<Collider2D>().enabled = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // 给玩家造成伤害
            collision.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);

            // 击退玩家
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