using PathBerserker2d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardArea : MonoBehaviour
{

    BoxCollider2D coll;
    public GuardEnemy guard;

    private void OnDrawGizmos()
    {
        coll = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.2f);
        Gizmos.DrawCube(transform.position, coll.size);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            guard.playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            guard.playerInArea = false;
            guard.GetComponent<NavAgent>();
        }
    }
}
