using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent((typeof(PlayerInput)))]

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed, moveDrag;
    [Header("Jump Variables")]
    [SerializeField] float jumpForce, fallMultiplier, jumpVelocityFallOff;
    float jumpPressTimer;
    [SerializeField] float jumpPressTimerAmount;
    [SerializeField] float downwardVelocityCap = 15f;

    [Header("Ground Check Variables")]
    [SerializeField] Transform raycastStartPos;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask raycastLayer;
    bool isGrounded;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var moveX = playerInput.actions.FindAction("Move").ReadValue<Vector2>().x;
        if (moveX != 0)
        {
            rb.velocity = new Vector2(1 * moveX * moveSpeed, rb.velocity.y);
        }
        //horizontal drag
        var vel = rb.velocity;
        vel.x *= 1.0f - moveDrag; // reduce x component...
        rb.velocity = vel;

        

        //jump velocity fall off
        if (!isGrounded && rb.velocity.y < jumpVelocityFallOff)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        //cap fall speed
        if(rb.velocity.y < downwardVelocityCap)
        {
            rb.velocity = new Vector2(rb.velocity.x, downwardVelocityCap);
        }
    }

    private void Update()
    {
        GroundCheck();

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
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPos.position, Vector2.down, raycastLength, raycastLayer);
        Debug.DrawRay(raycastStartPos.position, Vector2.down * raycastLength, Color.red);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

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
