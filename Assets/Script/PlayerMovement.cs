using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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
    public Transform feetPos;
    public float checkRadius;
    public float jumpPower;
    public bool isOnGround;
    public LayerMask groundLayer;
    float scaleX;
    int jumpCount;//��Ծ����
    bool jumpPress; //����״̬
    private float jumpTimeCounter;
    public float jumpTime;
    bool isjumping;


    public float baseJumpPower = 5f;
    public float jumpPowerIncrement = 0.2f;
    public float maxSpacePressDuration = 1f; // ����I�r�g

    //Dush
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private Vector2 moveDeraction;

    float moveX;

    public int canTransmit = 2;

    private float _fallSpeedYDampingChangeThreshold;


    public float distance;
    public LayerMask LadderMask;
    private bool isClimbing;
    float inputVertical;
    public bool isTrigger;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public float fallMultiplier;//���н�����ٶ�
    public float lowJumpMultiplier;//��Ծ�߶ȵ����Ƽ�����ֵԽ������Խ����
    public bool pressJump;
    public int jumpNum;//һ����������
    public int jumpRemainNum;//����������
    private bool hasDoubleJumped; // ���׷ۙ�Ƿ��ѽ��M���˶�����
    private bool isJumping;
    private void Start()
    {
        _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;
    }
    private void Awake()
    {
        canTransmit = 2; 
        scaleX = transform.localScale.x;
    }
    void Update()
    {
        if (isDashing) { return; }
        
        //if (Input.GetButtonDown("Jump") && jumpCount > 0)
        //{
        //    jumpPress = true;
        //}
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if(rg.velocity.y<_fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rg.velocity.y >= 0f &&!CameraManager.instance.IsLerpingYDamping&&CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }

        pressJump = Input.GetButton("Jump");
        Jump();
    }


    private void FixedUpdate()
    {

        if (isDashing) { return; }
        ProcessInputs();
        Move();
        isOnGroundCheck();
        Climb();

        
    }


    void ProcessInputs()
    {
        moveX = Input.GetAxis("Horizontal");//x������
        //float moveY = Input.GetAxisRaw("Vertical");//y������

        moveDeraction = new Vector2(moveX, 0).normalized; //��λ����


        //��ת���壨�����������
        if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            //transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    void Move()
    {
        rg.velocity = new Vector2(moveX * speed, rg.velocity.y); //�˶�����
    }



  

    //void Jump()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        if (isOnGround)
    //        {
    //            jumpRemainNum = jumpNum;
    //        }
    //        if (pressJump && jumpRemainNum-- > 0)
    //        {
    //            rg.velocity = new Vector2(rg.velocity.x, jumpPower);
    //        }
    //    }
    //    if (rg.velocity.y < 0)
    //    {
    //        rg.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;

    //    }
    //    else if (rg.velocity.y > 0 && !pressJump)
    //    {
    //        rg.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
    //    }
    //}
    void Jump()
    {
        if (isOnGround)
        {
            coyoteTimeCounter = coyoteTime;
            hasDoubleJumped = false; // ���ö�������ӛ
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // ���Ӷ������ęz��
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f && !isJumping) // һ����
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpPower);
                jumpBufferCounter = 0f;
                StartCoroutine(JumpCooldown());
            }
            else if (!hasDoubleJumped && !isOnGround) // ������
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpPower);
                jumpBufferCounter = 0f;
                hasDoubleJumped = true; // ��ӛ���M�ж�����
                StartCoroutine(JumpCooldown());
            }
        }

        if (Input.GetButtonUp("Jump") && rg.velocity.y > 0f)
        {
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (rg.velocity.y < 0)
        {
            rg.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;

        }
    }


    void Climb()
    {
        if (isTrigger)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isClimbing = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    isClimbing = false;
                }
            }

            if (isClimbing == true)
            {
                inputVertical = Input.GetAxisRaw("Vertical");
                rg.velocity = new Vector2(rg.velocity.x, inputVertical * speed);
                rg.gravityScale = 0;

            }
            else
            {
                rg.gravityScale = 1;
            }
        }
    }
    void isOnGroundCheck()
    {
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
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

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}
