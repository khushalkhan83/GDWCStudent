using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormPickup : MonoBehaviour
{
    [SerializeField] PlayerState form;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().ToggleState(form);
        }
    }
}
