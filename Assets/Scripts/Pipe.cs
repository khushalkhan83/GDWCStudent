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
    bool playerInEntrace = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInEntrace = true;
        buttonPromptCanvas.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInEntrace = false;
        buttonPromptCanvas.enabled = false;
    }

    private void Update()
    {
        if(playerInEntrace)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = transform.position;
                MovePlayerThroughPipe();
                playerInEntrace = false;
                buttonPromptCanvas.enabled = false;
            }
        }


        if(movePlayer)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
