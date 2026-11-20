using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private GameObject deathEffect;
    [SerializeField]
    private float deathEffectDuration = 6.6f;

    [SerializeField] private int deathSpawnWeight = 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }   
    }

    private void Death()
    {
        Debug.Log("Enemy Died");
        EnemyManager.Instance.SpawnEnemy(deathSpawnWeight);
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDuration);
        }
        Destroy(this.gameObject);
    }
}
