using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IHealth
{
    float currentHealth; 
    float maxHealth = 100f;
    [SerializeField] Transform startPos;
    [SerializeField] Slider hpBar;
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(hpBar != null)
        {
            hpBar.value = currentHealth;
        }
        
        if (currentHealth <= 0)
        {
            Died();
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (hpBar != null)
        {
            hpBar.value = currentHealth;
        }
    }

    private void Died()
    {
        Console.WriteLine("Player has been eliminated.");
        Reload();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startPos == null)
        {
            startPos = transform;
        }
        Reload();
    }

    void Reload()
    {
        currentHealth = maxHealth;
        transform.position = startPos.position;
        if (hpBar != null)
        {
            hpBar.maxValue = maxHealth;
            hpBar.value = currentHealth;
        }
    }

}
