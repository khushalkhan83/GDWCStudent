using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloudMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed, moveDrag;

    RaycastHit2D ray;
    [SerializeField] Transform raycastStartPosition;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask raycastLayer;
    public float targetHeight;
    [SerializeField] float hoverStrength, descendMultiplier;


    bool jumpHeld = false;
   
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Hover();
        Move();
        HandleHeight();
    }

    private void HandleHeight()
    {
        //check if not in heat block
        if(ray.collider != null)
        {
            if(ray.collider.isTrigger)
            {
                return;
            }
        }
        //handle raising height 
        if (jumpHeld)
        {
            targetHeight += hoverStrength * Time.deltaTime;
            if (targetHeight > 4)
            {
                targetHeight = 4;
            }
        }
        else
        {
            targetHeight -= (hoverStrength * descendMultiplier) * Time.deltaTime;
            if (targetHeight < 1)
            {
                targetHeight = 1;
            }
        }
    }

    private void Move()
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
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpHeld = true;
        }
        else if (context.canceled)
        {
            jumpHeld = false;
        }
    }

    public void Hover()
    {
        ray = Physics2D.Raycast(raycastStartPosition.position, Vector2.down, raycastLength, raycastLayer);

        if (transform.position.y > targetHeight)
        {
            if (ray.collider != null)
            {
                if (!ray.collider.isTrigger)
                {
                    rb.position = Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, ray.point.y + targetHeight), (hoverStrength * descendMultiplier) * Time.deltaTime);
                }
                else
                {
                    rb.position = Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, ray.point.y + targetHeight), hoverStrength * Time.deltaTime);
                }
            }
            else
            {
                rb.position = Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, ray.point.y + targetHeight), (hoverStrength * descendMultiplier) * Time.deltaTime);
            }
        }
        else
        {
            rb.position = Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, ray.point.y + targetHeight), hoverStrength * Time.deltaTime);
        }
    }

}
