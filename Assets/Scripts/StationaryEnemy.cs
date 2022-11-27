using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    [SerializeField] float sightLength;
    [SerializeField] LayerMask layersToCheck;

    [SerializeField] GameObject projectilePrefab;
    Transform target;
    [SerializeField] float projectileForce = 50f;
    [SerializeField] float projectileCooldown;
    float coolDownTimer;

    private void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RaycastHit2D hit = Physics2D.Linecast
                    (transform.position, collision.transform.position, layersToCheck);
            Debug.DrawLine(transform.position, collision.transform.position);
            if (hit.collider)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    target = hit.collider.transform;
                    if(coolDownTimer < 0)
                    {
                        Shoot();
                        coolDownTimer = projectileCooldown;
                    }
                }
                else
                {
                    target = null;
                }
            }
        }
    }

    void Shoot()
    {
        var newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        var dir = target.position - transform.position;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileForce);
    }

}
