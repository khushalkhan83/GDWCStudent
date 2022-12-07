using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;
    [SerializeField] GameObject player;
    private void Awake()
    {
        //Do not destroy on load
        int gameSessionCount = FindObjectsOfType<LevelWinCanvas>().Length;
        if (gameSessionCount > 1)
        {

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded +=

        player = GameObject.FindGameObjectWithTag("Player");
        if(currentCheckpoint !=null)
        {
            player
        }
    }

    void PlacePlayer()
    {

    }


}
