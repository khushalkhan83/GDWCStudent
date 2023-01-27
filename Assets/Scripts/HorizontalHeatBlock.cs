using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalHeatBlock : MonoBehaviour
{
    [SerializeField] float force;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().position += new Vector2(force * Time.deltaTime, 0);

        }
    }

}
