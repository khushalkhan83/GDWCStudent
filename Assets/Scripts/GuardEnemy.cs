using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathBerserker2d;

public class GuardEnemy : MonoBehaviour
{
    public bool playerInArea;
    Transform defaultPosition;

    private void Awake()
    {
        GameObject newObj = new GameObject("Default Position");
        defaultPosition = Instantiate(newObj, transform.position, Quaternion.identity).transform;
    }
    private void Update()
    {
        if(playerInArea)
        {
            GetComponent<GoalWalker>().goal = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            GetComponent<GoalWalker>().goal = defaultPosition;
        }
    }


}
