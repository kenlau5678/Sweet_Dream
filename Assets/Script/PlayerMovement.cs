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
    public Rigidbody2D rg; //Rigidbody2D 对象
    public Collider2D coll; //Collider2D 对象
    public TrailRenderer tr;
    public float speed; //speed


    //Jump
    public Transform feetPos;
    public float checkRadius;
    public float jumpPower;
    public bool isOnGround;
    public LayerMask groundLayer;
    float scaleX;
    int jumpCount;//跳跃次数
    bool jumpPress; //按键状态
    private float jumpTimeCounter;
    public float jumpTime;
    bool isjumping;


    public float baseJumpPower = 5f;
    public float jumpPowerIncrement = 0.2f;
    public float maxSpacePressDuration = 1f; // 最大按鍵時間

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

    public float fallMultiplier;//空中降落的速度
    public float lowJumpMultiplier;//跳跃高度的限制级别（数值越大跳的越矮）
    public bool pressJump;
    public int jumpNum;//一共能跳几次
    public int jumpRemainNum;//还能跳几次
    private bool hasDoubleJumped; // 用於追蹤是否已經進行了二段跳
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
        moveX = Input.GetAxis("Horizontal");//x轴向量
        //float moveY = Input.GetAxisRaw("Vertical");//y轴向量

        moveDeraction = new Vector2(moveX, 0).normalized; //单位向量


        //倒转物体（包括子物件）
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
        rg.velocity = new Vector2(moveX * speed, rg.velocity.y); //运动方向
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
            hasDoubleJumped = false; // 重置二段跳標記
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

        // 增加二段跳的檢查
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f && !isJumping) // 一段跳
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpPower);
                jumpBufferCounter = 0f;
                StartCoroutine(JumpCooldown());
            }
            else if (!hasDoubleJumped && !isOnGround) // 二段跳
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpPower);
                jumpBufferCounter = 0f;
                hasDoubleJumped = true; // 標記已進行二段跳
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
