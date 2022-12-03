using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(FindObjectOfType<LevelWinCanvas>().FadeOut(2f));
        }
    }
}
