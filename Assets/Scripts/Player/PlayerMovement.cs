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
    }

    void Update()
    {
        PlayerHorizontalMovement();
        UpdateAnimationState();
    }
    private void PlayerHorizontalMovement()
    {
        dirX = joystick.Horizontal;
        playerRB.velocity = new Vector2(dirX * moveSpeed, playerRB.velocity.y);

       /* dirX = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(dirX * moveSpeed, playerRB.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }*/
    }
    public void PlayerJump()
    {
        if (jumpCount < maxJumpCount)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x,jumpForce);
        }
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
        if (dirX > 0f)
        {
            state = MovementState.RUN;
            playerSprite.flipX = true;
        }
        else if (dirX < 0f)
        {
            state = MovementState.RUN;
            playerSprite.flipX = false;
        }
        else
        {
            state = MovementState.IDLE;
        }
        if (playerRB.velocity.y > 0.1f)
        {
            state = MovementState.JUMP;
        }
        else if (playerRB.velocity.y < -0.1f)
        {
            state = MovementState.FALL;
        }
        if(isCollided)
        {
            state = MovementState.HIT;
            isCollided = false;
        }
        playerAnim.SetInteger("state", (int)state);
    }

    
}
