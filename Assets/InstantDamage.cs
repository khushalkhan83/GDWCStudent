using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamage : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] bool playerInvincibleFrames = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().UpdateHealth(-damage, playerInvincibleFrames);
        }
    }

    

}
