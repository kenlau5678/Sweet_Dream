using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class PlayerMovement : MonoBehaviour
{
    //Object 对象相关变更量
    public Rigidbody2D rg; //Rigidbody2D 对象
    public Collider2D coll; //Collider2D 对象
    public TrailRenderer tr; //Trail Renderer对象
    public Animator animator; //Animator 对象
    public ParticleSystem dust;
    public GameObject dashShadow;

    //Move 移动相关变量
    public float speed; //角色移动速度
    float scaleX;//玩家当前面向方向
    float moveX;//x轴方向
    private Vector2 moveDeraction;//运动方向

    //Jump 跳跃相关变量
    public Transform feetPos;//Player的子对象，在Player底下，用于检测是否在地板Layer上
    public float checkRadius;//feetPos检测半径
    public LayerMask groundLayer;//地板Layer
    public float jumpPower;//跳跃的力
    public bool isOnGround;//表示是否在地上
    public float maxSpacePressDuration = 1f; // 最大按鍵時間
    public float coyoteTime = 0.2f;//土狼时间（离开地面的土狼时间内还能跳跃）
    private float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public float fallMultiplier;//空中降落的速度
    public float lowJumpMultiplier;//跳跃高度的限制级别（数值越大跳的越矮）
    public bool pressJump;
    public int jumpNum;//一共能跳几次
    public int jumpRemainNum;//还能跳几次
    private bool hasDoubleJumped; // 用於追蹤是否已經進行了二段跳
    private bool isJumping;//表示是否在跳跃
    float inputVertical;

    //Dush 冲刺相关变量
    private bool canDash = true;
    private bool isDashing;//表示是否在冲刺
    public float dashingPower;//冲刺给的力
    public float dashingTime;//冲刺时间
    public float dashingCooldown;//冲刺冷却时间

    public int UpOrDown = 1;
    //Transmit 传送相关变量
    public int canTransmit = 2;

    //Camera 相机相关变量
    private float _fallSpeedYDampingChangeThreshold;

    //Ladder 楼梯相关变量
    public LayerMask LadderMask;//楼梯Layer
    private bool isClimbing;//表示是否在爬行状态
    public bool isTrigger;//表示是否可以爬行，当楼梯触发到玩家会变成true

    public Sprite dashShadowIMG;
    public Sprite reverseDashShadowIMG;

    public Vector3 SavePos;

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
        if (isDashing) { return; }//确保如果在冲刺期间，角色不会有其他一点
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());//触发冲刺函数
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


    public void creatDust()
    {
        dust.Play();
    }
    private void FixedUpdate()
    {
        if (isDashing) { return; }//确保如果在冲刺期间，角色不会有其他一点
        ProcessInputs();

        Move();
        isOnGroundCheck();
        Climb();    
    }

    //处理玩家输入
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");//x轴向量
        //float moveY = Input.GetAxisRaw("Vertical");//y轴向量

        moveDeraction = new Vector2(moveX, 0).normalized; //单位向量

        //倒转物体（包括子物件）
        if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            scaleX = -1;
            //transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
            dashShadow.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, reverseDashShadowIMG);
        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            scaleX = 1;
            dashShadow.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, dashShadowIMG);
            //transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }

        //Whether is runing
        if (moveDeraction == Vector2.zero)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }
    }

    //玩家运动函数
    void Move()
    {
        rg.velocity = new Vector2(moveX * speed, rg.velocity.y); //设置运动速度、方向， x为左右方向乘以速度，y为当前y方向速度
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

    //跳跃函数
    //1. 实现土狼时间
    //2. 实现按键时长决定跳跃高度
    void Jump()
    {
        if (isOnGround)
        {
            coyoteTimeCounter = coyoteTime;//土狼时间计时器
            hasDoubleJumped = false; // 重置二段跳標記    
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;//土狼时间器倒数
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
                creatDust();
                AudioManager.instance.PlaySFX("Jump");
                StartCoroutine(JumpCooldown());//开始跳跃冷却函数

            }
            else if (!hasDoubleJumped && !isOnGround) // 二段跳
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpPower);
                jumpBufferCounter = 0f;
                hasDoubleJumped = true; // 標記已進行二段跳
                StartCoroutine(JumpCooldown());//开始跳跃冷却函数


            }
        }

        if (Input.GetButtonUp("Jump") && rg.velocity.y > 0f)
        {
            //按键时长决定跳跃高度
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        //rg.velocity.y小于零时
        //即玩家在下落状态时
        
        if (rg.velocity.y < 0)
        {
            //使玩家下落速度更快，手感更好
            rg.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            animator.SetBool("IsDown", true);
        }
        else
        {
            animator.SetBool("IsDown", false);
        }
    }

    //攀爬函数
    void Climb()
    {
        if (isTrigger)//当触发到楼梯时isTrigger为true
        {
            if (Input.GetKey(KeyCode.W))//W键为爬楼梯
            {
                isClimbing = true;
            }
            else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))//A、D键取消攀爬
            {
                isClimbing = false;
            }

            if (isClimbing == true)
            {
                inputVertical = Input.GetAxisRaw("Vertical");
                rg.velocity = new Vector2(rg.velocity.x, inputVertical * speed);//向上的速度
                rg.gravityScale = 0;//攀爬时取消重力
            }
            else
            {
                rg.gravityScale = 1;//非攀爬状态时gravityScale变回原本数值
            }
        }
        else
        {
            rg.gravityScale = 1;//离开楼梯时gravityScale变回原本数值
        }
    }

    //判断是否在地上
    void isOnGroundCheck()
    {
        //检差给定圆心(feetPos.position)和半径范围(checkRadius)内有没有groundLayer对象
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        animator.SetBool("IsJumping", !isOnGround);//如果不在地上，就播放跳跃动画
        tr.emitting = !isOnGround;

    }

    //冲刺函数
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashShadow.SetActive(true);
        float originalGravity = rg.gravityScale;
        rg.gravityScale = 0f;
        rg.velocity = new Vector2(scaleX * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rg.gravityScale = originalGravity;
        isDashing = false;
        dashShadow.SetActive(false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    //跳跃冷却
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);//冷却时间
        isJumping = false;
    }

    public void ChangeG(int UD)
    {
        UpOrDown = UD;
        Physics2D.gravity = new Vector2(0f, -1 * UpOrDown * Physics2D.gravity.y);
    }
}
