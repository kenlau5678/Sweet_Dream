using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightElevator : MonoBehaviour
{
    private bool flag = true;
    public float coolDownTime;
    public Rigidbody2D rb;
    // Start is called before the first frame update
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
            if (flag)
            {
                rb.DOMoveX(transform.position.x + 3, 1);
                flag = false;
            }
            else
            {
                
                rb.DOMoveX(transform.position.x - 3, 1);
                flag = true;

            }
            yield return new WaitForSeconds(coolDownTime);
        }


    }

}
