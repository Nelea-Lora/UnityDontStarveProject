using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalOrPlantManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private HealthSystem _healthSystem;
    private AnimalOrPlant _animalOrPlantTmp;
    private float _damageAdd;
    private bool _attackAdded;
    void Start()
    {
        _playerCollision.OnAnimalOrPlantTriggerEnter += OnAnimalOrPlantTriggerEnter;
        _playerCollision.OnAnimalOrPlantTriggerExit += OnAnimalOrPlantTriggerExit;
    }
    
    void Update()
    {
        if (_animalOrPlantTmp && _animalOrPlantTmp &&
            _animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Attaker && !_attackAdded)
        {
            DamageAdd();
            _attackAdded = true;
        }
    }
    void OnAnimalOrPlantTriggerEnter(AnimalOrPlant animalOrPlant)
    {
        _attackAdded = false;
        _animalOrPlantTmp = animalOrPlant;
    }
    void OnAnimalOrPlantTriggerExit(AnimalOrPlant animalOrPlant)
    {
        _animalOrPlantTmp = null;
    }

    public void DamageAdd()
    {
        Attacker attackerComponent = _animalOrPlantTmp.animalPlant as Attacker;
        if (attackerComponent != null)
        {
            _healthSystem.TakeDamage(attackerComponent.damageAmount);
        }
    }
}
