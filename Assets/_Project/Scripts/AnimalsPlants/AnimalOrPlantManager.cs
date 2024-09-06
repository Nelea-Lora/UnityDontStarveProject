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
                _playerController.itemInHands.itemType == ItemType.Instrument && Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamageAP();
            }
        }
    }
    void OnAnimalOrPlantTriggerEnter(AnimalOrPlant animalOrPlant)
    {
        _attackAdded = false;
        _animalOrPlantTmp = animalOrPlant;
        _animalOrPlantTmp.animalPlant.healthLevel = _animalOrPlantTmp.animalPlant.maxHealthLevel;
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
        if(_animalOrPlantTmp.animalPlant.healthLevel<=0)return;
        InstrumentItem instrumentItem = _playerController.itemInHands as InstrumentItem;
        if (!instrumentItem) return;
        if (_animalOrPlantTmp.animalPlant.animalPlantType is AnimalPlantType.Attaker or AnimalPlantType.Runner)
        {
            _animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;
        }
        else if (_animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Vegetable)
        {
            Vegetable vegetable = _animalOrPlantTmp.animalPlant as Vegetable;
            if(vegetable && vegetable.instrumentType == instrumentItem.instrumentType)
            {_animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;}
        }
        if (_animalOrPlantTmp.animalPlant.healthLevel <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        print("Death");
        if(_animalOrPlantTmp.animalPlant.items==null) return;
        print(_animalOrPlantTmp.animalPlant+" death");
        for (int i = 0; i < _animalOrPlantTmp.animalPlant.items.Count; i++)
        {
           Item item = _animalOrPlantTmp.animalPlant.items[i];
           print("You've got "+item);
           Instantiate(item.gameObject, _animalOrPlantTmp.transform.position, Quaternion.identity);
        }
        Destroy(_animalOrPlantTmp.gameObject);
    }
    
}
