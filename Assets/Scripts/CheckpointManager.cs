using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;
    public Transform levelSpawnPos;
    [SerializeField] GameObject player;
    private void Awake()
    {
        //Do not destroy on load
        int gameSessionCount = FindObjectsOfType<CheckpointManager>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            levelSpawnPos = GameObject.FindGameObjectWithTag("Level Spawn").transform;
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SpawnPlayer;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SpawnPlayer;
    }

    public void SpawnPlayer(Scene scene, LoadSceneMode mode)
    {

        levelSpawnPos = GameObject.FindGameObjectWithTag("Level Spawn").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (currentCheckpoint != null)
            {
                player.transform.position = currentCheckpoint.position;
                Destroy(currentCheckpoint.gameObject);
            }
            else
            {
                player.transform.position = levelSpawnPos.position;
            }
        }
    }
}