using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterAnimation : MonoBehaviour
{
    Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    float lockState;


    bool isGrounded, jumpStarted;
    float verticalVelocity, horizontalVelocity;

    [SerializeField] float fidgetTime;
    float fidgetTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fidgetTimer = fidgetTime;
    }

    private void Update()
    {
        //count down lockstate
        lockState -= Time.deltaTime;

        isGrounded = GetComponent<PlayerMovement>().isGrounded;
        verticalVelocity = GetComponent<Rigidbody2D>().velocity.y;
        horizontalVelocity = GetComponent<PlayerInput>().actions.FindAction("Move").ReadValue<Vector2>().x;

        if (horizontalVelocity < 0f)
        {
            spriteRenderer.transform.localScale = new Vector3(-Mathf.Abs(spriteRenderer.transform.localScale.x), spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
        }
        else if(horizontalVelocity > 0f) 
        {
            spriteRenderer.transform.localScale = new Vector3(Mathf.Abs(spriteRenderer.transform.localScale.x), spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
        }

        if (lockState <= 0f)
        {
            if(isGrounded)
            {
                if(Mathf.Abs(horizontalVelocity) > 0f)
                {
                    animator.CrossFade("Water_Walk", 0f);
                    fidgetTimer = fidgetTime;
                }
                else
                {
                    if (fidgetTimer > 0f)
                    {
                        animator.CrossFade("Water_Idle", 0f);
                        fidgetTimer -= Time.deltaTime;
                        if (fidgetTimer <= 0f)
                        {
                            animator.CrossFade("Water_Fidget", 0f);
                        }
                    }
                }
                jumpStarted = false;
            }
            else
            {
                if(verticalVelocity > 0f && !jumpStarted)
                {
                    animator.CrossFade("Water_Jump", 0f);
                    jumpStarted = true;
                    fidgetTimer = fidgetTime;
                }
                else if(verticalVelocity<0f)
                {
                    animator.CrossFade("Water_Fall", 0f);
                    jumpStarted = false;
                    fidgetTimer = fidgetTime;
                }
            }
        }
    }


}
