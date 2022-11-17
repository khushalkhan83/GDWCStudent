using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    [SerializeField] Transform[] path;
    [SerializeField] GameObject pathPipe;
    List<GameObject> pathPipes = new List<GameObject>();


    public void UpdatePipe()
    {
        foreach(GameObject previousPipe in pathPipes)
        {
            Destroy(previousPipe.gameObject);
        }
        foreach(Transform point in path)
        {
            var newPathPipe = Instantiate(pathPipe, point.position, Quaternion.identity);
            newPathPipe.transform.parent = transform;
            pathPipes.Add(newPathPipe);
        }
    }
}
