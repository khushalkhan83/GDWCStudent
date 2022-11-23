using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHP = 3;
    [SerializeField] int startHP = 2;
    float currentHP;
    [SerializeField] Image[] healthImages;
    [SerializeField] Sprite healthFull, healthEmpty;
    private void Awake()
    {
        currentHP = startHP;
        UpdateHealthUI();
    }

    public void UpdateHealth(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        UpdateHealthUI();
        if(currentHP <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < maxHP; i++)
        {
            if (currentHP > i)
            {
                healthImages[i].sprite = healthFull;
            }
            else
            {
                healthImages[i].sprite = healthEmpty;
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
