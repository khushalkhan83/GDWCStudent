using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Pipe : MonoBehaviour
{

    [SerializeField] GameObject pathPipePrefab;
    bool movePlayer = false;
    GameObject player;
    [SerializeField] Transform endPoint;
    [SerializeField] float pipeSpeed = 2f;
    [SerializeField] Vector3 exitForce;
    [SerializeField] Canvas buttonPromptCanvas;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        buttonPromptCanvas.enabled = true;
        if (collision.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                MovePlayerThroughPipe();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonPromptCanvas.enabled = false;
    }

    private void Update()
    {

        if(movePlayer)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position ,endPoint.position, pipeSpeed * Time.deltaTime );
            if(player.transform.position == endPoint.position)
            {
                player.GetComponent<BoxCollider2D>().enabled = true;
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<Rigidbody2D>().gravityScale = 1;
                player.GetComponent<Rigidbody2D>().AddForce(exitForce, ForceMode2D.Impulse);
                movePlayer = false;
            }
        }
    }

    void MovePlayerThroughPipe()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        movePlayer = true;
        
    }
}
