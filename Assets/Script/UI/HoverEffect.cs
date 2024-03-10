using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public float moveDistance = 1f; // �ƶ��ľ���
    public float moveDuration = 2f; // �����ƶ��ĳ���ʱ��
    public bool flag = true; 
    public Rigidbody2D rb;
    public float coolDownTime;
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
        while (true)
        {
            if (flag)
            {
                rb.DOMoveY(transform.position.y + moveDistance, moveDuration);
               flag = false;
            }
            else 
            {

                rb.DOMoveY(transform.position.y - moveDistance,moveDuration);
                flag = true;

            }
            yield return new WaitForSeconds(coolDownTime);
        }


    }
}
