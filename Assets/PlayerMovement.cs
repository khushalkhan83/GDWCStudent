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
    [SerializeField] float jumpForce, fallMultiplier, jumpVelocityFallOff;

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
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
