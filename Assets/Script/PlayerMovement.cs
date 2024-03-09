using DG.Tweening;
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
    //Object ������ر����
    public Rigidbody2D rg; //Rigidbody2D ����
    public Collider2D coll; //Collider2D ����
    public TrailRenderer tr; //Trail Renderer����
    public Animator animator; //Animator ����
    public ParticleSystem dust;
    public GameObject dashShadow;

    //Move �ƶ���ر���
    public float speed; //��ɫ�ƶ��ٶ�
    float scaleX;//��ҵ�ǰ������
    float dashFoward=1;
    float moveX;//x�᷽��
    private Vector2 moveDeraction;//�˶�����

    //Jump ��Ծ��ر���
    public Transform feetPos;//Player���Ӷ�����Player���£����ڼ���Ƿ��ڵذ�Layer��
    public float checkRadius;//feetPos���뾶
    public LayerMask groundLayer;//�ذ�Layer
    public float jumpPower;//��Ծ����
    public bool isOnGround;//��ʾ�Ƿ��ڵ���
    public float maxSpacePressDuration = 1f; // ����I�r�g
    public float coyoteTime = 0.2f;//����ʱ�䣨�뿪���������ʱ���ڻ�����Ծ��
    private float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public float fallMultiplier;//���н�����ٶ�
    public float lowJumpMultiplier;//��Ծ�߶ȵ����Ƽ�����ֵԽ������Խ����
    public bool pressJump;
    public int jumpNum;//һ����������
    public int jumpRemainNum;//����������
    private bool hasDoubleJumped; // ���׷ۙ�Ƿ��ѽ��M���˶�����
    private bool isJumping;//��ʾ�Ƿ�����Ծ
    float inputVertical;

    //Dush �����ر���
    private bool canDash = true;
    private bool isDashing;//��ʾ�Ƿ��ڳ��
    public float dashingPower;//��̸�����
    public float dashingTime;//���ʱ��
    public float dashingCooldown;//�����ȴʱ��

    public int UpOrDown = 1;
    //Transmit ������ر���
    public int canTransmit = 2;

    //Camera �����ر���
    private float _fallSpeedYDampingChangeThreshold;

    //Ladder ¥����ر���
    public LayerMask LadderMask;//¥��Layer
    private bool isClimbing;//��ʾ�Ƿ�������״̬
    public bool isTrigger;//��ʾ�Ƿ�������У���¥�ݴ�������һ���true

    public Sprite dashShadowIMG;
    public Sprite reverseDashShadowIMG;

    public Vector3 SavePos;

    public PlatformManager platformManager;

    public Transform followPoint;
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
        if (isDashing) { return; }//ȷ������ڳ���ڼ䣬��ɫ����������һ��
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());//������̺���
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
        if (isDashing) { return; }//ȷ������ڳ���ڼ䣬��ɫ����������һ��
        ProcessInputs();

        Move();
        isOnGroundCheck();
        Climb();    
    }

    //�����������
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");//x������
        //float moveY = Input.GetAxisRaw("Vertical");//y������

        moveDeraction = new Vector2(moveX, 0).normalized; //��λ����

        //��ת���壨�����������
        if (moveX < 0)
        {
            followPoint.DORotate(new Vector3(0, -180, 0),0.5f);
            //transform.rotation = Quaternion.Euler(0, -180, 0);
            dashFoward = -1;
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
            dashShadow.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, reverseDashShadowIMG);

        }
        else if (moveX > 0)
        {
            followPoint.DORotate(new Vector3(0, 0, 0), 0.5f);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            dashFoward = 1;
            dashShadow.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, dashShadowIMG);
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
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

    //����˶�����
    void Move()
    {
        rg.velocity = new Vector2(moveX * speed, rg.velocity.y); //�����˶��ٶȡ����� xΪ���ҷ�������ٶȣ�yΪ��ǰy�����ٶ�
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

    //��Ծ����
    //1. ʵ������ʱ��
    //2. ʵ�ְ���ʱ��������Ծ�߶�
    void Jump()
    {
        if (isOnGround)
        {
            coyoteTimeCounter = coyoteTime;//����ʱ���ʱ��
            hasDoubleJumped = false; // ���ö�������ӛ    
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;//����ʱ��������
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
                if (platformManager != null)
                {
                    platformManager.TriggerPlatformChangeAppearing();
                }
                jumpBufferCounter = 0f;
                creatDust();
                AudioManager.instance.PlaySFX("Jump");

                StartCoroutine(JumpCooldown());//��ʼ��Ծ��ȴ����

            }
            else if (!hasDoubleJumped && !isOnGround) // ������
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpPower);
                if (platformManager != null)
                {
                    platformManager.TriggerPlatformChangeAppearing();
                }
                jumpBufferCounter = 0f;
                hasDoubleJumped = true; // ��ӛ���M�ж�����

                StartCoroutine(JumpCooldown());//��ʼ��Ծ��ȴ����


            }
        }

        if (Input.GetButtonUp("Jump") && rg.velocity.y > 0f)
        {
            //����ʱ��������Ծ�߶�
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        //rg.velocity.yС����ʱ
        //�����������״̬ʱ
        
        if (rg.velocity.y < 0)
        {
            //ʹ��������ٶȸ��죬�ָи���
            rg.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            animator.SetBool("IsDown", true);
        }
        else
        {
            animator.SetBool("IsDown", false);
        }
    }

    //��������
    void Climb()
    {
        if (isTrigger)//��������¥��ʱisTriggerΪtrue
        {
            if (Input.GetKey(KeyCode.W))//W��Ϊ��¥��
            {
                isClimbing = true;
            }
            else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))//A��D��ȡ������
            {
                isClimbing = false;
            }

            if (isClimbing == true)
            {
                inputVertical = Input.GetAxisRaw("Vertical");
                rg.velocity = new Vector2(rg.velocity.x, inputVertical * speed);//���ϵ��ٶ�
                rg.gravityScale = 0;//����ʱȡ������
            }
            else
            {
                rg.gravityScale = 1;//������״̬ʱgravityScale���ԭ����ֵ
            }
        }
        else
        {
            rg.gravityScale = 1;//�뿪¥��ʱgravityScale���ԭ����ֵ
        }
    }

    //�ж��Ƿ��ڵ���
    void isOnGroundCheck()
    {
        //������Բ��(feetPos.position)�Ͱ뾶��Χ(checkRadius)����û��groundLayer����
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        animator.SetBool("IsJumping", !isOnGround);//������ڵ��ϣ��Ͳ�����Ծ����
        tr.emitting = !isOnGround;

    }

    //��̺���
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashShadow.SetActive(true);
        float originalGravity = rg.gravityScale;
        rg.gravityScale = 0f;
        rg.velocity = new Vector2(dashFoward * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rg.gravityScale = originalGravity;
        isDashing = false;
        dashShadow.SetActive(false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    //��Ծ��ȴ
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);//��ȴʱ��
        isJumping = false;
    }

    public void ChangeG(int UD)
    {
        UpOrDown = UD;
        Physics2D.gravity = new Vector2(0f, -1 * UpOrDown * Physics2D.gravity.y);
    }
}
