using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifeCycles : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private RecipeManager _recipeManager;
    private  CampfireManager _campfireManager;
    
    void Start()
    {
        _playerCollision.OnItemTriggerEnter += CampfireEnter;
        _playerCollision.OnItemTriggerExit += CampfireExit;
    }
    void Update()
    {
        if (_playerController && _playerController.itemInHands && _inventoryManager &&
            (_playerController.itemInHands.burnLevel > 0 || _playerController.itemInHands.itemType == ItemType.Food)
            &&_campfireManager &&_campfireManager.DistanceToPlayer < 1 && Input.GetKeyDown(KeyCode.Space))
        {
            if(_playerController.itemInHands.burnLevel > 0)
            {
                float burn = _campfireManager.
                    Burn(_campfireManager.maxLevelFire / _playerController.itemInHands.burnLevel);
                print("burnLevel "+burn);
            }
            if(_playerController.itemInHands.itemType == ItemType.Food && _recipeManager)
            {
                _recipeManager.Cook(_playerController.itemInHands);
            }
            _inventoryManager.UseItemAndDecrease();
        }
    }

    private void CampfireEnter(Item item)
    {
        if (item.TryGetComponent(out CampfireManager campfireManager))
        {
            _campfireManager = campfireManager;
        }
    }

    private void CampfireExit(Item item)
    {
        _campfireManager = null;
    }
}
