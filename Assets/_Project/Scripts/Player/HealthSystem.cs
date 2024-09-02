using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image _health;
    [SerializeField] private Image _hunger;
    [SerializeField] private Image _mind;
    public float maxHealth = 1;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }
    void UpdateHealthUI()
    {
        if (_health)
        {
            _health.fillAmount = currentHealth;
        }
    }
    void Die()
    {
        Debug.Log("Player died");
    }
}
