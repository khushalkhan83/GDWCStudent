using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupContainer : MonoBehaviour
{
    public int totalPickups;

    private void Awake()
    {
        //Do not destroy on load
        int gameSessionCount = FindObjectsOfType<PickupContainer>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            transform.SetParent(FindObjectOfType<CheckpointManager>().transform);
        }

        totalPickups = FindObjectsOfType<Pickup>().Length;
    }
}
