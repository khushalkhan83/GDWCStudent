using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score;
    [SerializeField] TextMeshProUGUI scoreText;

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = amount.ToString("000");
    }

    public int GetScore()
    {
        return score;
    }


}
