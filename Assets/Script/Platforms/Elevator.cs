using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool isTrigger;
    public bool flag = true;
    public float moveDirection;
    public float moveTime;
    public bool canMove= true ;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.E)&&canMove)
        {
            StartCoroutine(ElevatorMove());
        }
    }

    private IEnumerator ElevatorMove()
    {
        // 禁用触发器
        canMove = false;

        if (flag)
        {
            transform.DOMoveY(transform.position.y - moveDirection, moveTime);
            flag = false;
        }
        else
        {
            transform.DOMoveY(transform.position.y + moveDirection, moveTime);
            flag = true;
        }
        yield return new WaitForSeconds(moveTime);

        // 启用触发器
        canMove = true;
    }
}
