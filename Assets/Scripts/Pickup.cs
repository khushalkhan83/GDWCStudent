using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int scoreAmount = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //increment score
            FindObjectOfType<Score>().UpdateScore(scoreAmount);
            //disable object
            gameObject.SetActive(false);
        }
    }

}
