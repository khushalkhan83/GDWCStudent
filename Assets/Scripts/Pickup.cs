using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int scoreAmount = 1;

    public void Awake()
    {
        //check for scene state script to see if collected in previous level attempt?
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FindObjectOfType<Score>().UpdateScore(scoreAmount);
            gameObject.SetActive(false);
        }
    }

}
