using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [SerializeField] float timeTilDamage = 2f;
    float timer;

    private void Awake()
    {
        timer = timeTilDamage;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                collision.collider.GetComponent<Health>().UpdateHealth(-1);
                timer = timeTilDamage;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        timer = timeTilDamage;
    }
}
