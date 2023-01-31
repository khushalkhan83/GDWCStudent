using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent = FindObjectOfType<CheckpointManager>().transform;
        FindObjectOfType<CheckpointManager>().currentCheckpoint = transform;
        GetComponent<SpriteRenderer>().color = Color.red;
        DontDestroyOnLoad(gameObject);
    }
}
