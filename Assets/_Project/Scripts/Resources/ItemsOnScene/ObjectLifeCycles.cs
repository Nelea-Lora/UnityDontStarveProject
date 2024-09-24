using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifeCycles : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerCollision _playerCollision;
    private  CampfireManager _campfireManager;
    
    void Start()
    {
        _playerCollision.OnItemTriggerEnter += CampfireEnter;
        _playerCollision.OnItemTriggerExit += CampfireExit;
    }
    void Update()
    {
        if (_playerController && _playerController.itemInHands && _playerController.itemInHands.burnLevel > 0
            &&_campfireManager &&_campfireManager.DistanceToPlayer < 1 && Input.GetKeyDown(KeyCode.Space))
        {
            float burn = _campfireManager.
                Burn(_campfireManager.maxLevelFire/_playerController.itemInHands.burnLevel);
            _inventoryManager.UseItem();
            print("burnLevel "+burn);
        }
        // if(_campfireManager&& _campfireManager._Item )
        // {
        //     print("//////_campfireManager/////");
        //     if( _inventoryManager.itemTmp._Item==_campfireManager._Item ) 
        //         print("*****_inventoryManager.itemTmp._Item==_campfireManager._item******");
        // }
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
