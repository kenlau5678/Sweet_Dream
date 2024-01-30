using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //Object
    public Rigidbody2D rg; //Rigidbody2D ����
    public Collider2D coll; //Collider2D ����
    public TrailRenderer tr;
    public float speed; //speed




    private Vector2 moveDeraction;
    void Update()
    {
    
        ProcessInputs();
    }


    private void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");//x������
        //float moveY = Input.GetAxisRaw("Vertical");//y������

        moveDeraction = new Vector2(moveX, 0).normalized; //��λ����
    }

    void Move()
    {
        rg.velocity = new Vector2(moveDeraction.x * speed, rg.velocity.y); //�˶�����
    }
}
