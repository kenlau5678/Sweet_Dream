using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightElevator : MonoBehaviour
{
    public float coolDownTime;
    public Rigidbody2D rb;
    public float moveDistance;
    public float moveTime;
    // Start is called before the first frame update
    public LeftRight leftRight;
    public enum LeftRight
    {
        Left,
        Right
    }
    void Start()
    {
        StartCoroutine(LeftRightMove());
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private IEnumerator LeftRightMove()
    {
        while(true) 
        {
            if (leftRight == LeftRight.Right)
            {
                rb.DOMoveX(transform.position.x + moveDistance, moveTime);
                leftRight = LeftRight.Left;
            }
            else if (leftRight == LeftRight.Left)
            {
                
                rb.DOMoveX(transform.position.x - moveDistance, moveTime);
                leftRight = LeftRight.Right;

            }
            yield return new WaitForSeconds(coolDownTime);
        }


    }

}
