using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Pipe : MonoBehaviour
{

    [SerializeField] Transform[] path;
    [SerializeField] GameObject pathPipePrefab;
    [SerializeField] List<GameObject> pathPipes = new List<GameObject>();
    bool movePlayer = false;
    GameObject player;
    Vector3 playerTarget;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MovePlayerThroughPipe();
        }

        if(movePlayer)
        {
            player.transform.Translate(playerTarget);
        }
    }

    void MovePlayerThroughPipe()
    {
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        player.GetComponent<BoxCollider2D>().enabled = false;
        movePlayer = true;
        foreach(GameObject pathPipe in pathPipes)
        {
            playerTarget = pathPipe.transform.position;
            while(player.transform.position != playerTarget)
            {
                Debug.Log("Moving");
            }
        }
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        movePlayer = false;
    }

    public void UpdatePipe()
    {
        foreach(GameObject previousPipe in pathPipes)
        {
            DestroyImmediate(previousPipe.gameObject);
        }
        foreach(Transform point in path)
        {
            var newPathPipe = Instantiate(pathPipePrefab, point.position, Quaternion.identity);
            newPathPipe.transform.parent = transform;
            pathPipes.Add(newPathPipe);
        }
    }
}
