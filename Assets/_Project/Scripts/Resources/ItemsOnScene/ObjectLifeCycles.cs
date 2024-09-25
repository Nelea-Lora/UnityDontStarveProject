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
        if (_playerController && _playerController.itemInHands && Input.GetKeyDown(KeyCode.Space)) 
            BurnItem(_campfireManager, _playerController.itemInHands);
    }

    public void BurnItem(CampfireManager campfireManager, ItemScriptableObject itemInHands)
    {
        if (itemInHands&& _inventoryManager &&campfireManager &&campfireManager.DistanceToPlayer < 1 &&
            (itemInHands.burnLevel > 0 || itemInHands.itemType == ItemType.Food))
        {
            if(itemInHands.burnLevel > 0)
            {
                float burn = campfireManager.
                    Burn(campfireManager.maxLevelFire / itemInHands.burnLevel);
                print("burnLevel "+burn);
            }
            if(itemInHands.itemType == ItemType.Food && _recipeManager)
            {
                _recipeManager.Cook(itemInHands);
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
