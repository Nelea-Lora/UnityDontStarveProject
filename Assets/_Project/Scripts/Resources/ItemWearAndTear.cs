using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWearAndTear : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private NightmersConroller _nightmersConroller;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _shelfLife;
    
    void Update()
    {
        FoodShelfLife();
    }

    private void FoodShelfLife()
    {
        if (_inventoryManager&&_inventoryManager.slots is null) return;
        foreach (InventorySlot slot in _inventoryManager.slots)
        {
            if(!slot.item || slot.item.currTimeShelfLife<=0)continue;
            if (slot.item.itemType == ItemType.Food ||
                (slot.item.itemType == ItemType.Light && _nightmersConroller 
                                                      && _nightmersConroller.PointLightStatus))
            {
                slot.item.currTimeShelfLife -= Time.time * _shelfLife;
            }
            else if (slot.item.itemType == ItemType.Instrument && Input.GetKeyDown(KeyCode.Space))
            {
                slot.item.currTimeShelfLife -= _shelfLife;
            }
            if(slot.item.currTimeShelfLife<=0)
            {
                if (slot.amount == 1 &&_playerController && _playerController.itemInHands && 
                    slot.item == _playerController.itemInHands)
                {
                    print("another object");
                    _playerController.ItIsAnotherObjectInHand();
                    if(_playerController.itemInHands)print("Item in hands "+_playerController.itemInHands );
                }
                slot.DecreaseSlotData(1);
            }
        }
    }
}
