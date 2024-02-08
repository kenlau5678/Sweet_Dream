using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //Object
    public Rigidbody2D rg; //Rigidbody2D ����
    public Collider2D coll; //Collider2D ����
    public TrailRenderer tr;
    public float speed; //speed


    //Jump
    public float jumpPower;
    public bool isOnGround;
    public LayerMask groundLayer;
    float scaleX;
    int jumpCount;//��Ծ����
    bool jumpPress; //����״̬

    //Dush
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private Vector2 moveDeraction;

    float moveX;

    public int canTransmit = 1;
    private void Awake()
    {
        scaleX = transform.localScale.x;
    }
    void Update()
    {
        if (isDashing) { return; }
        
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPress = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    private void FixedUpdate()
    {

        if (isDashing) { return; }
        ProcessInputs();
        Move();
        isOnGroundCheck();
        Jump();
    }


    void ProcessInputs()
    {
        moveX = Input.GetAxis("Horizontal");//x������
        //float moveY = Input.GetAxisRaw("Vertical");//y������

        moveDeraction = new Vector2(moveX, 0).normalized; //��λ����


        //��ת���壨�����������
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    void Move()
    {
        rg.velocity = new Vector2(moveX * speed, rg.velocity.y); //�˶�����
    }


    void Jump()
    {
        //�ڵ�����
        if (isOnGround)
        {
            jumpCount = 1;

        }

        //�ڵ�������Ծ
        if (jumpPress && isOnGround)
        {

            rg.velocity = new Vector2(rg.velocity.x * speed, jumpPower);

            jumpCount--;
            jumpPress = false;
            //Debug.Log(jumpCount);

        }
        //�ڿ�����Ծ
        else if (jumpPress && jumpCount >= 0 && !isOnGround)
        {
            rg.velocity = new Vector2(rg.velocity.x * speed, jumpPower);
            jumpCount--;
            jumpPress = false;
            //Debug.Log(jumpCount);
        }
    }


    void isOnGroundCheck()
    {
        //�жϽ�ɫ��ײ�������ͼ�㷢���Ӵ�
        if (coll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rg.gravityScale;
        rg.gravityScale = 0f;
        rg.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rg.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
