using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] float timeTilHeal = 2f;
    Animator animator;
    float timer;
    bool used = false;

    private void Awake()
    {
        timer = timeTilHeal;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !used)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                collision.GetComponent<Health>().UpdateHealth(1);
                timer = timeTilHeal;
                used = true;
                animator.SetBool("Used", used);
            }
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = timeTilHeal;
    }
}
