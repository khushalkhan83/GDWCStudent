using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] float timeTilHeal = 2f;
    float timer;

    private void Awake()
    {
        timer = timeTilHeal;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                collision.GetComponent<Health>().UpdateHealth(1);
                timer = timeTilHeal;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = timeTilHeal;
    }
}
