using System.Collections.Generic;
using _Project.Scripts.Player.Observer;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IHealthSubject
{
    private List<IHealthObserver> _observers = new List<IHealthObserver>();

    // [SerializeField] private Image _health;
    // [SerializeField] private Image _hunger;
    // [SerializeField] private Image _mind;

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
        NotifyObservers(); // уведомить сразу
    }

    private void Update()
    {
        GettingHungry();
        if (day != null && day.DayProgress() > 0.4f) LoseMind();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        NotifyObservers();
    }

    private void GettingHungry()
    {
        _currentHunger -= _timeHunger;
        if (_currentHunger < 0.2f) TakeDamage(0.00002f);
        NotifyObservers();
    }

    private void LoseMind()
    {
        _currentMind -= 0.00001f;
        if (_currentMind < 0.2f) TakeDamage(0.00002f);
        NotifyObservers();
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
        NotifyObservers();
    }

    public void Eat(float amount)
    {
        _currentHunger = Mathf.Min(_currentHunger + amount, maxHealth);
        NotifyObservers();
    }

    public void IncreaseMind(float amount)
    {
        _currentMind = Mathf.Min(_currentMind + amount, maxHealth);
        NotifyObservers();
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    // === Реализация Observer ===
    public void Attach(IHealthObserver observer)
    {
        if (!_observers.Contains(observer)) _observers.Add(observer);
    }

    public void Detach(IHealthObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.OnHealthChanged(_currentHealth, _currentHunger, _currentMind);
        }
    }
    
    public float GetCurrentHealth() => _currentHealth;
    public float GetCurrentHunger() => _currentHunger;
    public float GetCurrentMind() => _currentMind;

    public void SetAll(float health, float hunger, float mind)
    {
        _currentHealth = health;
        _currentHunger = hunger;
        _currentMind = mind;
        NotifyObservers();
    }
}

