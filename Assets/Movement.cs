using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //Object
    public Rigidbody2D rg; //Rigidbody2D 对象
    public Collider2D coll; //Collider2D 对象
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
        float moveX = Input.GetAxisRaw("Horizontal");//x轴向量
        //float moveY = Input.GetAxisRaw("Vertical");//y轴向量

        moveDeraction = new Vector2(moveX, 0).normalized; //单位向量
    }

    void Move()
    {
        rg.velocity = new Vector2(moveDeraction.x * speed, rg.velocity.y); //运动方向
    }
}
