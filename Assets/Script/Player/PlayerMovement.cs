using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class PlayerMovement : MonoBehaviour
{
    // Objects
    public Rigidbody2D rg; // Rigidbody2D Object
    public Collider2D coll; // Collider2D Object
    public TrailRenderer tr; // Trail Renderer Object
    public Animator animator; // Animator Object
    public ParticleSystem dust;
    public GameObject dashShadow;

    // Move parameters
    public float speed;
    float scaleX; // Face direction X
    float dashForward = 1; // Dash direction
    private Vector2 moveDirection; // Move direction
    public float moveX;
    // Jump parameters
    public Transform feetPos; // Player's feet position
    public float checkRadius; // Radius for ground check
    public LayerMask groundLayer; // Ground layer
    public float jumpPower; // Jump power
    public bool isOnGround; // Indicates if on ground
    public float maxSpacePressDuration = 1f; // Max duration for holding space
    public float coyoteTime = 0.2f; // Time allowed to jump after leaving ground
    private float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    public float fallMultiplier; // Fall speed multiplier
    public float lowJumpMultiplier; // Low jump multiplier
    public bool pressJump;
    public int jumpNum; // Number of jumps allowed
    public int jumpRemainNum; // Remaining jumps
    private bool hasDoubleJumped; // Indicates if double jumped
    private bool isJumping; // Indicates if jumping

    // Dash parameters
    private bool canDash = true;
    private bool isDashing; // Indicates if dashing
    public float dashingPower; // Dash power
    public float dashingTime; // Dash duration
    public float dashingCooldown; // Dash cooldown

    public int UpOrDown = 1;
    public int canTransmit = 2; // Transmission ability

    // Ladder parameters
    public LayerMask LadderMask; // Ladder layer
    private bool isClimbing; // Indicates if climbing
    public bool isTrigger; // Indicates if in ladder trigger area

    public Sprite dashShadowIMG;
    public Sprite reverseDashShadowIMG;

    public Vector3 SavePos;

    public PlatformManager platformManager;

    public Transform followPoint;
    private float _fallSpeedYDampingChangeThreshold;

    public bool isHit = false;

    PlayerHit playerHitComponent;
    private void Start()
    {
        // ��ȡ PlayerHit ������洢��һ��������
        playerHitComponent = GetComponent<PlayerHit>();
        _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;
    }



    private bool CheckForActionBlockers()
    {
        return playerHitComponent.isHit || isDashing;
    }

    private void Awake()
    {
        canTransmit = 2;
        scaleX = transform.localScale.x;
    }

    void Update()
    {
        if (CheckForActionBlockers()) return;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash()); // Start dash coroutine
        }

        UpdateCameraDamping();

        pressJump = Input.GetButton("Jump");
        Jump();
    }

    private void FixedUpdate()
    {
        if (CheckForActionBlockers()) return;
        ProcessInputs();
        Move();
        isOnGroundCheck();
        Climb();
    }

    // Process input for movement and rotation
    void ProcessInputs()
    {
        moveX = 0; // Reset moveX to 0

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }
        if (DialogueManager.Instance.isDialogueActive)
        {
            moveX = 0;
        }
        moveDirection = new Vector2(moveX, 0).normalized; // Unit vector for movement

        // Flip character and set dash direction
        if (moveX < 0)
        {
            followPoint.DORotate(new Vector3(0, -180, 0), 0.5f);
            dashForward = -1;
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
            dashShadow.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, reverseDashShadowIMG);
        }
        else if (moveX > 0)
        {
            followPoint.DORotate(new Vector3(0, 0, 0), 0.5f);
            dashForward = 1;
            dashShadow.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, dashShadowIMG);
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }

        // Set animation state for running and control walk sound effect
        bool isRunning = moveDirection != Vector2.zero;
        animator.SetBool("IsRun", isRunning);
        if (isRunning && !AudioManager.instance.IsPlayingSFX("Walk")&&isOnGround)
        {
            AudioManager.instance.PlaySFXLoop("Walk"); // Ensure this method plays the sound effect in loop
        }
        else if (!isRunning)
        {
            AudioManager.instance.StopSFX("Walk");
        }
        else
        {
            AudioManager.instance.StopSFX("Walk");
        }
    }


    // Move the player
    void Move()
    {
        rg.velocity = new Vector2(moveX * speed, rg.velocity.y);
    }

    // Jump logic
    void Jump()
    {
        if (DialogueManager.Instance.isDialogueActive)
        {
            return;
        }
        UpdateCoyoteTime();
        UpdateJumpBuffer();

        ProcessJumpInput();
        ApplyExtraGravity();
        LimitJumpHeight();
    }

    // ����Coyoteʱ���߼�
    void UpdateCoyoteTime()
    {
        if (isOnGround)
        {
            coyoteTimeCounter = coyoteTime; // ����Coyoteʱ��
            hasDoubleJumped = false; // ����˫��״̬
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // ����Coyoteʱ��
        }
    }

    // ������Ծ�����߼�
    void UpdateJumpBuffer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime; // ������Ծ����
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime; // ������Ծ����
        }
    }

    // ������Ծ����
    void ProcessJumpInput()
    {
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f && !isJumping)
            {
                PerformJump();
                jumpBufferCounter = 0; // ʹ�ú�������Ծ����
            }
            else if (!hasDoubleJumped && !isOnGround)
            {
                PerformJump();
                hasDoubleJumped = true;
                jumpBufferCounter = 0; // ʹ�ú�������Ծ����
            }
        }
    }

    // Ӧ�ö��������Լ�������
    void ApplyExtraGravity()
    {
        if (rg.velocity.y < 0)
        {
            rg.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            animator.SetBool("IsDown", true);
        }
        else
        {
            animator.SetBool("IsDown", false);
        }
    }
    void LimitJumpHeight()
    {
        if (Input.GetButtonUp("Jump") && rg.velocity.y > 0f)
        {
            rg.velocity = new Vector2(rg.velocity.x, rg.velocity.y * 0.5f); // �������ϵ��ٶ�
            coyoteTimeCounter = 0f; // ȷ�������ٴδ���Coyoteʱ����Ծ
        }
    }
    // Perform a jump action
    private void PerformJump()
    {
        rg.velocity = new Vector2(rg.velocity.x, jumpPower);

        if (platformManager != null)
        {
            platformManager.TriggerPlatformChangeAppearing();
        }
        jumpBufferCounter = 0f;
        creatDust();
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX("Jump");
        }
        if (AchievementSystem.Instance != null)
        {
            AchievementSystem.Instance.JumpAchieve(); // ��������һ����Ծ�ɾ͵��߼�
        }
        StartCoroutine(JumpCooldown());
    }
    public void creatDust()
    {
        dust.Play();
    }
    // Climb logic for ladders
    void Climb()
    {
        if (isTrigger)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isClimbing = true;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                isClimbing = false;
            }

            if (isClimbing)
            {
                float inputVertical = Input.GetAxisRaw("Vertical");
                rg.velocity = new Vector2(rg.velocity.x, inputVertical * speed);
                rg.gravityScale = 0;
            }
            else
            {
                rg.gravityScale = 1;
            }
        }
        else
        {
            rg.gravityScale = 1;
        }
    }

    // Check if the player is on the ground
    void isOnGroundCheck()
    {
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        animator.SetBool("IsJumping", !isOnGround);
        animator.SetBool("IsOnGround", isOnGround);
        tr.emitting = !isOnGround;
    }

    // Dash coroutine
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashShadow.SetActive(true);
        float originalGravity = rg.gravityScale;
        rg.gravityScale = 0f;
        rg.velocity = new Vector2(dashForward * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rg.gravityScale = originalGravity;
        isDashing = false;
        dashShadow.SetActive(false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    // Cooldown for jumping
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    // Update camera damping based on player's fall speed
    private void UpdateCameraDamping()
    {
        if (rg.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rg.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }
    }

    // Change gravity direction
    public void ChangeG(int UD)
    {
        UpOrDown = UD;
        Physics2D.gravity = new Vector2(0f, -1 * UpOrDown * Physics2D.gravity.y);
    }
}
