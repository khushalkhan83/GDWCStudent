using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerState
{
    Water,
    Cloud
}
[RequireComponent((typeof(PlayerInput)))]
public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;
    [SerializeField] public PlayerState playerState;

    [Header("Cloud Movement Variables")]
    [SerializeField] float cloudHorizontalSpeed;
    [SerializeField] float cloudVerticalSpeed;
    [SerializeField] float cloudDrag;

    [Space(50)]

    [Header("Water Movement Variables")]
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDrag;
    [Header("Water Jump Variables")]
    [SerializeField] float jumpForce, fallMultiplier, jumpVelocityFallOff;
    float jumpPressTimer;
    [SerializeField] float jumpPressTimerAmount;
    [SerializeField] float downwardVelocityCap = 15f;

    [Header("Ground Check Variables")]
    [SerializeField] Transform[] raycastStartPositions;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask raycastLayer;
    public bool isGrounded;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (playerState == PlayerState.Water)
        {
            GroundCheck();
            WaterJump();
        }
        else if (playerState == PlayerState.Cloud)
        {

        }
    }

    private void FixedUpdate()
    {
        if (playerState == PlayerState.Water)
        {
            rb.gravityScale = 1f;
            WaterMovement();
            WaterJumpRestraints();
        }
        else if(playerState == PlayerState.Cloud)
        {
            rb.gravityScale = 2f;
            CloudMovement();
        }
    }

    public void ToggleState(PlayerState newState)
    {
        playerState = newState;
    }


    private void WaterJumpRestraints()
    {
        //jump velocity fall off
        if (!isGrounded && rb.velocity.y < jumpVelocityFallOff)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        //cap fall speed
        if (rb.velocity.y < downwardVelocityCap)
        {
            rb.velocity = new Vector2(rb.velocity.x, downwardVelocityCap);
        }
    }

    private void WaterMovement()
    {
        var moveX = playerInput.actions.FindAction("Move").ReadValue<Vector2>().x;
        if (moveX != 0)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }
        //horizontal drag
        var vel = rb.velocity;
        vel.x *= 1.0f - moveDrag; // reduce x component...
        rb.velocity = vel;
    }
    private void CloudMovement()
    {
        var moveX = playerInput.actions.FindAction("Move").ReadValue<Vector2>().x;
        var moveY = playerInput.actions.FindAction("Move").ReadValue<Vector2>().y;

        if (moveX != 0 || moveY != 0)
        {
            rb.velocity = new Vector2(moveX * cloudHorizontalSpeed, moveY * cloudVerticalSpeed);
        }
        var vel = rb.velocity;
        vel.x *= 1.0f - cloudDrag;
        vel.y *= 1f - cloudDrag;
        rb.velocity = vel;
    }

    private void WaterJump()
    {
        jumpPressTimer -= Time.deltaTime;
        if (jumpPressTimer > 0 && isGrounded)
        {
            jumpPressTimer = 0;
            Debug.Log("Jump!");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void GroundCheck()
    {
        bool atleastOneGrounded = false;
        foreach (Transform pos in raycastStartPositions)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos.position, Vector2.down, raycastLength, raycastLayer);
            Debug.DrawRay(pos.position, Vector2.down * raycastLength, Color.red);
            if (hit.collider != null)
            {
                atleastOneGrounded = true;
            }
        }
        if(atleastOneGrounded)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    //public controls function for player input component
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpPressTimer = jumpPressTimerAmount;
        }
        else if (context.canceled)
        {
            //player released jump
            if (!isGrounded && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.5f);
            }
        }
    }
}
