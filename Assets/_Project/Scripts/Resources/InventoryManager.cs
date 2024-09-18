using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    public Transform _inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private Camera _mainCamera;
    private Item _itemTmp;
    [SerializeField] private DigitSwitching _digitSwitching;
    [SerializeField] private HealthSystem _healthSystem;
    private bool _itemAdded;

    void Start()
    {
        _playerCollision.OnItemTriggerEnter += OnItemTriggerEnter;
        _playerCollision.OnItemTriggerExit += OnItemTriggerExit;
        _mainCamera = Camera.main;
        for (int i = 0; i < _inventoryPanel.childCount; i++)
        {
            if (_inventoryPanel.GetChild(i).GetComponent<InventorySlot>()!=null)
            {
                slots.Add(_inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) GetItem();
        if (Input.GetKeyDown(KeyCode.Space)) UseItem();
    }

    private void GetItem()
    {
        if (!_itemTmp) return;
        AddItem(_itemTmp._Item, _itemTmp._amount);
        if (_itemAdded)Destroy(_itemTmp.gameObject);
        _itemAdded = false;
    }
    void OnItemTriggerEnter(Item item)
    {
        _itemTmp = item;
    }

    private void OnItemTriggerExit(Item item)
    {
        _itemTmp = null;
    }

    public void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount<=_item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
                    if (slot.item.maxTimeShelfLife > 0)
                    {
                        slot.item.currTimeShelfLife = slot.item.maxTimeShelfLife;
                        slot.ShelfMaxTime();
                        slot.UpdateShelfUI(slot.item.currTimeShelfLife);
                    }
                    _itemAdded = true;
                    return;
                }
                break;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            if (!slot.isComplete)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.SlotComplete();
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                if (slot.item.maxTimeShelfLife > 0)
                {
                    slot.item.currTimeShelfLife = slot.item.maxTimeShelfLife;
                    slot.ShelfMaxTime();
                    slot.UpdateShelfUI(slot.item.currTimeShelfLife);
                }
                _itemAdded = true;
                break;
            }
        }
    }

    private void UseItem()
    {
        if (!_digitSwitching) return;
        InventorySlot currentItem = _inventoryPanel.GetChild(_digitSwitching.currentSlotID)
            .GetComponent<InventorySlot>();
        if (!currentItem || !currentItem.item || !currentItem.itemAmount || !currentItem.iconGO 
            || currentItem.amount <= 0) return;
        if (currentItem.item.itemType == ItemType.Food)
        {
            FoodItem foodItem = currentItem.item as FoodItem;
            if(!foodItem)return;
            _healthSystem.Heal(foodItem.healingAmount);
            _healthSystem.Eat(foodItem.eatingAmount);
            _healthSystem.IncreaseMind(foodItem.mindAmount);
            if (currentItem.amount <= 1) currentItem.NullifySlotData();
            else currentItem.DecreaseSlotData(1);
        }
    }
    
}
