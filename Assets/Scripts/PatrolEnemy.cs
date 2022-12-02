using PathBerserker2d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    int targetPoint = 0;
    NavAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavAgent>();
    }

    private void Update()
    {
        if(navAgent.IsIdle)
        {
            if(GetComponent<MultiGoalWalker>().goals[0] == patrolPoints[0])
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
