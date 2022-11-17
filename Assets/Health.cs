using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHP = 3;
    float currentHP;
    [SerializeField] Image[] healthImages;
    private void Awake()
    {
        currentHP = maxHP;
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
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < maxHP; i++)
        {
            if (currentHP >= i)
            {
                healthImages[i].enabled = true;
            }
            else
            {
                healthImages[i].enabled = false;
            }
        }
    }
}
