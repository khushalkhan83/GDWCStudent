using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    [SerializeField] float sightLength;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 dir = (this.transform.position - collision.transform.position);
            Debug.Log(dir);
            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, sightLength);
            if(ray.collider != null)
            {
                Debug.Log(ray.collider.name);
            }
        }
    }
}
