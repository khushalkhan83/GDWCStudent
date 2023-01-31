using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score;
    [SerializeField] TextMeshProUGUI scoreText;
    PickupContainer pickupContainer;
    int maxScore;

    private void Awake()
    {
        maxScore = FindObjectOfType<PickupContainer>().totalPickups;
        pickupContainer = FindObjectOfType<PickupContainer>();
        score = pickupContainer.totalPickups - FindObjectsOfType<Pickup>().Length;
        scoreText.text = score.ToString("00") + "/" + maxScore.ToString("00");

    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("00") + "/" + maxScore.ToString("00");
    }

    public int GetScore()
    {
        return score;
    }


}
