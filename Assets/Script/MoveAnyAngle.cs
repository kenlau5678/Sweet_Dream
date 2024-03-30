using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MoveAnyAngle : MonoBehaviour
{
    public float coolDownTime;
    public Rigidbody2D rb;
    public float moveDistance;
    public float moveTime;
    public Vector2 moveDirection; // Direction of movement
    private bool movingForward = true; // Flag to track direction

    void Start()
    {
        StartCoroutine(MoveInDirection());
    }

    private IEnumerator MoveInDirection()
    {
        while (true)
        {
            Vector2 targetPosition;
            if (movingForward)
            {
                // Move the object in the specified direction
                targetPosition = (Vector2)transform.position + (moveDirection.normalized * moveDistance);
            }
            else
            {
                // Move the object in the opposite direction
                targetPosition = (Vector2)transform.position - (moveDirection.normalized * moveDistance);
            }

            rb.DOMove(targetPosition, moveTime);

            // Flip the direction for the next move
            movingForward = !movingForward;

            // Wait for the cooldown time before moving again
            yield return new WaitForSeconds(coolDownTime);
        }
    }
}
