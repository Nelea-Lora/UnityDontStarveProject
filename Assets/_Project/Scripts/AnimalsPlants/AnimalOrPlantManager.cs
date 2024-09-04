using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalOrPlantManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private PlayerController _playerController;
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
        if (_animalOrPlantTmp)
        {
            if (_animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Attaker && !_attackAdded)
            {
                GiveDamage(); _attackAdded = true;
            }
            if (_playerController&& _playerController.itemInHands&& 
                _playerController.itemInHands.itemType == ItemType.Instrument && Input.GetMouseButtonDown(0))
            {
                TakeDamageAP();
            }
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

    private void GiveDamage()
    {
        Attacker attackerComponent = _animalOrPlantTmp.animalPlant as Attacker;
        if (attackerComponent)
        {
            _healthSystem.TakeDamage(attackerComponent.damageAmount);
        }
    }

    private void TakeDamageAP()
    {
        print("You hit it");
        if(_animalOrPlantTmp.animalPlant.healthLevel<=0)return;
        print("_animalOrPlantTmp.animalPlant.healthLevel==0"+ _animalOrPlantTmp.animalPlant.healthLevel);
        InstrumentItem instrumentItem = _playerController.itemInHands as InstrumentItem;
        if (!instrumentItem) return;
        print("You have "+instrumentItem);
        if (_animalOrPlantTmp.animalPlant.animalPlantType is AnimalPlantType.Attaker or AnimalPlantType.Runner)
        {
            _animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;
            print("  _animalOrPlantTmp.animalPlant.healthLevel  = "+  _animalOrPlantTmp.animalPlant.healthLevel );
        }
        else if (_animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Vegetable)
        {
            Vegetable vegetable = _animalOrPlantTmp.animalPlant as Vegetable;
            if(vegetable && vegetable.instrumentType == instrumentItem.instrumentType)
            {_animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;}
            print("  _animalOrPlantTmp.animalPlant.healthLevel  = "+  _animalOrPlantTmp.animalPlant.healthLevel );
        }
        if (_animalOrPlantTmp.animalPlant.healthLevel == 0)
        {
            print(_animalOrPlantTmp.animalPlant+" is dead");
        }
    }
    
}
