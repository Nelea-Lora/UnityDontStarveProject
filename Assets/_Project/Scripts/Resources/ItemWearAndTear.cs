using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWearAndTear : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private AnimalOrPlantManager _animalOrPlant;
    [SerializeField] private NightmersConroller _nightmersConroller;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private DigitSwitching _digitSwitching;
    [SerializeField] private float _shelfLife;
    
    
    void Update()
    {
        ItemShelfLife();
    }

    private void ItemShelfLife()
    {
        if (!_inventoryManager&&_inventoryManager.slots is null) return;
        foreach (InventorySlot slot in _inventoryManager.slots)
        {
            if(!slot||!slot.item || slot.item.currTimeShelfLife<=0)continue;
            if ((slot.item.itemType == ItemType.Instrument || slot.item.itemType == ItemType.Light)&&
                _playerController && _playerController.itemInHands 
                &&0<_digitSwitching.tmpCurrentSlotID&&_digitSwitching.tmpCurrentSlotID<_inventoryManager.slots.Count
                && slot != _inventoryManager.slots[_digitSwitching.tmpCurrentSlotID]) continue;
            if (slot.item.itemType == ItemType.Food || (slot.item.itemType == ItemType.Light 
                                                        && _nightmersConroller 
                                                        && _nightmersConroller.PointLightStatus))
            {
                slot.item.currTimeShelfLife -= _shelfLife;
            }
            else if (slot.item.itemType == ItemType.Instrument && Input.GetKeyDown(KeyCode.Space) 
                                                               && _animalOrPlant.AttackByInstrument)
            {
                slot.item.currTimeShelfLife -= 0.1f;
            }
            ChangeShelfUI(slot);
            if(slot.item.currTimeShelfLife<=0)
            {
                if (slot.amount == 1 &&_playerController && _playerController.itemInHands && 
                    slot.item == _playerController.itemInHands)
                {
                    _playerController.ItIsAnotherObjectInHand();
                    if(_playerController.itemInHands)print("Item in hands "+_playerController.itemInHands );
                }
                slot.DecreaseSlotData(1);
                if(slot.item)ChangeShelfUI(slot);
            }
        }
    }

    private void ChangeShelfUI(InventorySlot slot)
    {
        if (slot.item.itemType == ItemType.Food)slot.UpdateShelfSliderUI(slot.item.currTimeShelfLife);
        if (slot.item.itemType is ItemType.Instrument or ItemType.Light)
        {
            slot.UpdateShelfPercentageUI(slot.item.maxTimeShelfLife, slot.item.currTimeShelfLife);
        }
    }
}
