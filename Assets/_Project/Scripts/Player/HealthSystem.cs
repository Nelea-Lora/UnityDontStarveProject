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
    [SerializeField] private float _timeHunger;
    [SerializeField] private DayTime day;
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
        if (day is not null && day.DayProgress() > 0.4) LoseMind();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        UpdateHealthUI(_currentHealth, _health);
    }

    private void GettingHungry()
    {
        _currentHunger -= Time.time*_timeHunger;
        if(_currentHunger<0.2)TakeDamage(0.00002f);
        UpdateHealthUI(_currentHunger, _hunger);
    }

    private void LoseMind()
    {
        _currentMind -= 0.00001f; 
        if(_currentMind<0.2)TakeDamage(0.00002f);
        UpdateHealthUI(_currentMind, _mind);
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

    public void Eat(float eat)
    {
        _currentHunger += eat;
        if (_currentHunger > maxHealth)
        {
            _currentHunger = maxHealth;
        }
        UpdateHealthUI(_currentHunger, _hunger);
    }

    public void IncreaseMind(float amount)
    {
        _currentMind += amount;
        if (_currentMind > maxHealth)
        {
            _currentMind = maxHealth;
        }
        UpdateHealthUI(_currentMind, _mind);
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
