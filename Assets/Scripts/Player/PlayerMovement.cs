using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum MovementState
{
    IDLE,
    RUN,
    HIT,
    JUMP,
    FALL
}
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    private BoxCollider2D playerCollider;
    private float dirX = 0f;
    private bool isCollided;
    private int maxJumpCount = 2;
    private int jumpCount = 0;
    private IMovementStrategy movementStrategy;
    private IJumpStrategy jumpStrategy;


    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform groundCheck;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();

        movementStrategy = new WalkMovement();   
        jumpStrategy = new DoubleJump();
    }

    void Update()
    {
        PlayerHorizontalMovement();
        UpdateAnimationState();
    }
    private void FixedUpdate()
    {
        CheckGrounded();
    }
    private void PlayerHorizontalMovement()
    {
        dirX = joystick.Horizontal;
        movementStrategy.Move(playerRB,dirX,moveSpeed);
    }
    public void PlayerJump()
    {
        jumpStrategy.Jump(playerRB, ref jumpCount, maxJumpCount, jumpForce);
    }
    private void  CheckGrounded()
    {
        bool isGround =  Physics2D.OverlapCircle(groundCheck.position, 0.2f, jumpableGround);
        if (isGround)
        {
            jumpCount = 0;
        }
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        // Early return for vertical movement (highest priority)
        if (playerRB.velocity.y > 0.1f)
        {
            state = MovementState.JUMP;
            playerAnim.SetInteger("state", (int)state);
            return;
        }

        if (playerRB.velocity.y < -0.1f)
        {
            state = MovementState.FALL;
            playerAnim.SetInteger("state", (int)state);
            return;
        }

        // Horizontal movement
        if (dirX > 0f)
        {
            state = MovementState.RUN;
            playerSprite.flipX = true;
            playerAnim.SetInteger("state", (int)state);
            return;
        }

        if (dirX < 0f)
        {
            state = MovementState.RUN;
            playerSprite.flipX = false;
            playerAnim.SetInteger("state", (int)state);
            return;
        }

        // Default to idle
        state = MovementState.IDLE;
        playerAnim.SetInteger("state", (int)state);
    
    
    }

    
}
