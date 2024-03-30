using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dart : MonoBehaviour
{
    public float coolDownTime; // Time before the dart returns
    public Rigidbody2D rb;
    public float moveDistance;
    public float moveTime;
    public Vector2 moveDirection; // Direction of movement
    private Vector2 originalPosition; // To store the original position of the dart

    void Start()
    {
        originalPosition = transform.position; // Store the original position at the start
    }

    public void ShootDart()
    {
        Vector2 targetPosition;
        targetPosition = (Vector2)transform.position + (moveDirection.normalized * moveDistance);

        rb.DOMove(targetPosition, moveTime).OnComplete(() =>
        {
            // Start the coroutine to return the dart after coolDownTime
            StartCoroutine(ReturnDartAfterCooldown());
        });
    }

    private IEnumerator ReturnDartAfterCooldown()
    {
        // Wait for the specified cooldown time
        yield return new WaitForSeconds(coolDownTime);
        transform.position = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ShootDart();
        }

    }
}
