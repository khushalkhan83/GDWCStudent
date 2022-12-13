using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloudMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody2D rb;

    [SerializeField] Transform raycastStartPosition;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask raycastLayer;
    public float targetHeight;
    [SerializeField] float hoverStrength;

    bool jumpHeld = false;
   
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Hover();
    
        if(jumpHeld)
        {
            targetHeight += hoverStrength * Time.deltaTime;
            if(targetHeight > 3)
            {
                targetHeight = 3;
            }
        }
        else
        {
            targetHeight -= hoverStrength * Time.deltaTime;
            if(targetHeight < 1)
            {
                targetHeight = 1;
            }
        }
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
        
        RaycastHit2D ray = Physics2D.Raycast(raycastStartPosition.position, Vector2.down, raycastLength, raycastLayer);
        rb.position = Vector2.MoveTowards(transform.position, new Vector2(rb.position.x, ray.point.y + targetHeight), hoverStrength * Time.deltaTime);
        Debug.Log(ray.point.y);
    }

}
