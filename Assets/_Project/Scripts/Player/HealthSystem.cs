using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image _health;
    [SerializeField] private Image _hunger;
    [SerializeField] private Image _mind;
    public float maxHealth = 1;
    private float _currentHealth;
    private float _currentHunger;
    private float _currentMind;
    [SerializeField] private float _timeHunder;
    void Start()
    {
        _currentHealth = maxHealth;
        _currentHunger = maxHealth;
        _currentMind = maxHealth;
        UpdateHealthUI(_currentHealth, _health);
        UpdateHealthUI(_currentHunger, _health);
        UpdateHealthUI(_currentMind, _health);
    }

    private void Update()
    {
        GettingHungry();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        print(_currentHealth);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        UpdateHealthUI(_currentHealth, _health);
    }

    public void GettingHungry()
    {
        _currentHunger -= Time.time*_timeHunder;
        UpdateHealthUI(_currentHunger, _hunger);
    }

    public void Heal(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        UpdateHealthUI(_currentHealth, _health);
    }
    void UpdateHealthUI(float currentAmount, Image sliderH)
    {
        if (sliderH)
        {
            sliderH.fillAmount = currentAmount;
        }
    }
    void Die()
    {
        Debug.Log("Player died");
    }
}
