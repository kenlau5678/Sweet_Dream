using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;
	public float health = 100f;

	public bool isFlipped = false;

	public float deathExplosionRadius = 10f;
	public Rigidbody2D rb;
	public float knockbackForce = 5f;
    public float intensity;
    public float shaketime;
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
        CameraShake.Instance.shakeCamera(intensity, shaketime);
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
}