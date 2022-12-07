using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    float horizontalVelocity, verticalVelocity;
    bool isGrounded;
    [SerializeField] SpriteRenderer sprite;
    PlayerInput playerInput;

    float lockTimer = 0f;


    bool jumpBegan = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        horizontalVelocity = GetComponent<Rigidbody2D>().velocity.x;
        verticalVelocity = GetComponent<Rigidbody2D>().velocity.y;
        isGrounded = GetComponent<PlayerMovement>().isGrounded;

        var moveStick = playerInput.actions.FindAction("Move").ReadValue<Vector2>();

        lockTimer -= Time.deltaTime;

        if (lockTimer > 0f)
        {
            return;
        }
        else
        {

            if (moveStick.x < 0)
            {
                sprite.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (moveStick.x > 0)
            {
                sprite.transform.localScale = new Vector3(1, 1, 1);
            }
            if (isGrounded)
            {
                if (jumpBegan)
                {
                    animator.CrossFade("player_land", 0f);
                    LockState(0.2f);
                }
                else
                {
                    if (Mathf.Abs(moveStick.x) > 0)
                    {
                        animator.CrossFade("player_walk", 0f);
                    }
                    else
                    {
                        animator.CrossFade("player_idle", 0f);
                    }
                }
                jumpBegan = false;

            }
            if (!isGrounded && verticalVelocity > 5 && !jumpBegan)
            {
                jumpBegan = true;
                animator.CrossFade("player_jump_begin", 0f);
            }
        }
    }

    void LockState(float length)
    {
        lockTimer = length;
    }

}
