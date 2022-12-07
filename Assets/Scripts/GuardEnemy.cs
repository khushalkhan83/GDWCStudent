using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathBerserker2d;

public class GuardEnemy : MonoBehaviour
{
    public bool playerInArea;
    [SerializeField] int damage;
    [SerializeField] Transform patrolRoute;
    List<Transform> patrolPoints = new List<Transform>();
    NavAgent navAgent;
    [SerializeField] SpriteRenderer alertSprite;

    private void Awake()
    {
        navAgent = GetComponent<NavAgent>();
        patrolRoute.GetComponent<GuardArea>().guard = this;

        foreach (Transform point in patrolRoute.GetComponentsInChildren<Transform>())
        {
            if (point.parent == patrolRoute)
            {
                patrolPoints.Add(point);
            }
        }

        GetComponent<MultiGoalWalker>().goals[0] = patrolPoints[0];


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Health>().canHit)
            {
                collision.GetComponent<Health>().UpdateHealth(-damage, true);
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (collision.transform.position.x < transform.position.x)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30f, 7.5f), ForceMode2D.Impulse);
                }
                else
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(30f, 7.5f), ForceMode2D.Impulse);
                }
            }
        }
    }

    private void Update()
    {
        
        if (playerInArea)
        {
            GetComponent<MultiGoalWalker>().goals[0] = GameObject.FindGameObjectWithTag("Player").transform;
            GetComponent<MultiGoalWalker>().enabled = false;
            GetComponent<GoalWalker>().enabled = true;
            alertSprite.enabled = true;

        }
        else
        {
            alertSprite.enabled = false;

            GetComponent<MultiGoalWalker>().enabled = true;
            GetComponent<GoalWalker>().enabled = false;

            if (navAgent.IsIdle)
            {
                if (GetComponent<MultiGoalWalker>().goals[0] == patrolPoints[0])
                {
                    GetComponent<MultiGoalWalker>().goals[0] = patrolPoints[1];
                }
                else
                {
                    GetComponent<MultiGoalWalker>().goals[0] = patrolPoints[0];
                }
                GetComponent<MultiGoalWalker>().MoveToClosestGoal();
            }
        }
    }


}
