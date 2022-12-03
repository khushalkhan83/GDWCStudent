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
    public bool canHit = true;

    [SerializeField] float invincibleTime = 1f;
    private void Awake()
    {
        currentHP = startHP;
        UpdateHealthUI();
    }

    public void UpdateHealth(int amount, bool invincible = false)
    {
        if(amount < 0 && !canHit)
        {
            return;
        }
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        UpdateHealthUI();
        if (currentHP <= 0)
        {
            Die();
        }
        if(invincible)
        {
            StartCoroutine(InvincibleFrames());
        }
    }

    IEnumerator InvincibleFrames()
    {
        canHit = false;
        for(float i = 0; i < invincibleTime; i += 0.2f)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        canHit = true;
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
