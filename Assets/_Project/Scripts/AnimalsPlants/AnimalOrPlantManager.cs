using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.AnimalsPlants.Components;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalOrPlantManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    // [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private PlayerController _playerController;
    private AnimalOrPlant _animalOrPlantTmp;
    private bool _attackAdded;
    public bool AttackByInstrument { get; private set; }
    
    void Start()
    {
        _playerCollision.OnAnimalOrPlantTriggerEnter += OnAnimalOrPlantTriggerEnter;
        _playerCollision.OnAnimalOrPlantTriggerExit += OnAnimalOrPlantTriggerExit;
    }
    
    void Update()
    {
        // if (_animalOrPlantTmp)
        // {
        //     if (_animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Attacker && !_attackAdded)
        //     {
        //         GiveDamage(); 
        //         _attackAdded = true;
        //     }
        //     AttackByInstrument = false;
        //     if (_playerController&& _playerController.itemInHands&& 
        //         _playerController.itemInHands.itemType == ItemType.Instrument && Input.GetKeyDown(KeyCode.Space))
        //     {
        //         TakeDamageAP();
        //     }
        // }
        if (_animalOrPlantTmp)
        {
            var attack = _animalOrPlantTmp.GetComponent<AttackComponent>();
            if (attack != null && !_attackAdded)
            {
                attack.TriggerAttack(_playerController);
                _attackAdded = true;
            }

            var tame = _animalOrPlantTmp.GetComponent<TameComponent>();
            if (tame != null)
            {
                tame.TryTame(_playerController);
            }

            AttackByInstrument = false;
            if (_playerController?.itemInHands is InstrumentItem instrument && Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamageAP(instrument);
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

    // private void GiveDamage()
    // {
    //     Attacker attackerComponent = _animalOrPlantTmp.animalPlant as Attacker;
    //     if (attackerComponent)
    //     {
    //         GameFacade.Instance.TakeDamage(attackerComponent.damageAmount);
    //         //_healthSystem.TakeDamage(attackerComponent.damageAmount);
    //     }
    // }

    private void TakeDamageAP(InstrumentItem instrumentItem)
    {
        // if(_animalOrPlantTmp.animalPlant.healthLevel<=0)return;
        // InstrumentItem instrumentItem = _playerController.itemInHands as InstrumentItem;
        // if (!instrumentItem) return;
        // if (_animalOrPlantTmp.animalPlant.animalPlantType is AnimalPlantType.Attacker or AnimalPlantType.Runner)
        // {
        //     _animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;
        //     AttackByInstrument = true;
        // }
        // else if (_animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Vegetable)
        // {
        //     Vegetable vegetable = _animalOrPlantTmp.animalPlant as Vegetable;
        //     if (vegetable && vegetable.instrumentType == instrumentItem.instrumentType)
        //     {
        //         _animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;
        //         AttackByInstrument = true;
        //     }
        // }
        // if (_animalOrPlantTmp.animalPlant.healthLevel <= 0)
        // {
        //     AttackByInstrument = false;
        //     Death();
        // }
        
        if (_animalOrPlantTmp.animalPlant.healthLevel <= 0) return;

        if (_animalOrPlantTmp.animalPlant.animalPlantType is AnimalPlantType.Attacker or AnimalPlantType.Runner)
        {
            _animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;
            AttackByInstrument = true;
        }
        else if (_animalOrPlantTmp.animalPlant.animalPlantType == AnimalPlantType.Vegetable)
        {
            var vegetable = _animalOrPlantTmp.animalPlant as Vegetable;
            if (vegetable?.instrumentType == instrumentItem.instrumentType)
            {
                _animalOrPlantTmp.animalPlant.healthLevel -= instrumentItem.forceAmount;
                AttackByInstrument = true;
            }
        }

        if (_animalOrPlantTmp.animalPlant.healthLevel <= 0)
        {
            AttackByInstrument = false;
            Death();
        }
    }

    private void Death()
    {
        // if(_animalOrPlantTmp.animalPlant.items==null) return;
        // print(_animalOrPlantTmp.animalPlant+"'s death");
        // for (int i = 0; i < _animalOrPlantTmp.animalPlant.items.Count; i++)
        // {
        //    Item item = _animalOrPlantTmp.animalPlant.items[i];
        //    print("You've got "+item);
        //    Instantiate(item.gameObject, _animalOrPlantTmp.transform.position, Quaternion.identity);
        // }
        // Destroy(_animalOrPlantTmp.gameObject);
        
        if (_animalOrPlantTmp.animalPlant.items == null) return;
        Debug.Log(_animalOrPlantTmp.animalPlant + "'s death");
        foreach (var item in _animalOrPlantTmp.animalPlant.items)
        {
            Debug.Log("You've got " + item);
            Instantiate(item.gameObject, _animalOrPlantTmp.transform.position, Quaternion.identity);
        }
        Destroy(_animalOrPlantTmp.gameObject);
    }
    
}
