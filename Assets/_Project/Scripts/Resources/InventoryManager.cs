using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerCollision _playerCollision;
    [SerializeField] private PlayerController _playerController;
    public Transform _inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public Item itemTmp;
    [SerializeField] private DigitSwitching _digitSwitching;
    [SerializeField] private HealthSystem _healthSystem;
    private bool _itemAdded;

    void Start()
    {
        _playerCollision.OnItemTriggerEnter += OnItemTriggerEnter;
        _playerCollision.OnItemTriggerExit += OnItemTriggerExit;
        for (int i = 0; i < _inventoryPanel.childCount; i++)
        {
            if (_inventoryPanel.GetChild(i).GetComponent<InventorySlot>()!=null)
            {
                slots.Add(_inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].slotID = i;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) GetItem();
        if (Input.GetKeyDown(KeyCode.Space)) UseItem();
    }

    private void GetItem()
    {
        if (!itemTmp) return;
        if (itemTmp && itemTmp._Item&& itemTmp._Item.itemType==ItemType.BuildItem) return;
        AddItem(itemTmp._Item, itemTmp._amount);
        if (_itemAdded)Destroy(itemTmp.gameObject);
        _itemAdded = false;
    }
    void OnItemTriggerEnter(Item item)
    {
        itemTmp = item;
    }

    private void OnItemTriggerExit(Item item)
    {
        itemTmp = null;
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
                    ChangeTimeShelfLife(slot);
                    if(slot==_digitSwitching.slotParent.GetChild(_digitSwitching.currentSlotID)
                           .GetComponent<InventorySlot>())
                        _digitSwitching.TakeItemInHands();
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
                if(_item.maximumAmount==1)slot.item = Instantiate(_item);
                else slot.item = _item;
                slot.amount = _amount;
                slot.SlotComplete();
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = _amount.ToString();
                ChangeTimeShelfLife(slot);
                if(slot==_digitSwitching.slotParent.GetChild(_digitSwitching.currentSlotID)
                       .GetComponent<InventorySlot>())
                    _digitSwitching.TakeItemInHands();
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
        if (currentItem.item.itemType == ItemType.Food && !itemTmp)
        {
            FoodItem foodItem = currentItem.item as FoodItem; if(!foodItem)return;
            print("you eat " + foodItem);
            _healthSystem.Heal(foodItem.healingAmount); _healthSystem.Eat(foodItem.eatingAmount);
            _healthSystem.IncreaseMind(foodItem.mindAmount);
            if (currentItem.amount <= 1)
            {
                currentItem.NullifySlotData();
                _playerController.ItIsAnotherObjectInHand();
            }
            else currentItem.DecreaseSlotData(1);
        }
    }

    public void UseItemAndDecrease()
    {
        if (!_digitSwitching || !_inventoryPanel ||!_playerController) return;
        InventorySlot currentItem = _inventoryPanel.GetChild(_digitSwitching.currentSlotID)
            .GetComponent<InventorySlot>();
        if (!currentItem || !currentItem.item || !currentItem.itemAmount || !currentItem.iconGO 
            || currentItem.amount <= 0) return;
        print("currentItem.item "+currentItem.item);
        if (currentItem.item.itemType == ItemType.Food || currentItem.item.burnLevel > 0)
        {
            print("currentItem.item.itemType == ItemType.Food || currentItem.item.burnLevel > 0");
            if (currentItem.amount <= 1)
            {
                currentItem.NullifySlotData();
                _playerController.ItIsAnotherObjectInHand();
            }
            else currentItem.DecreaseSlotData(1);
        }
    }
    private void ChangeTimeShelfLife(InventorySlot slot)
    {
        if (slot.item.maxTimeShelfLife > 0)
        {
            slot.item.currTimeShelfLife = slot.item.maxTimeShelfLife;
            if (slot.item.itemType == ItemType.Food)
            {
                slot.ShelfMaxTime();
                slot.UpdateShelfSliderUI(slot.item.currTimeShelfLife);
            }
            if (slot.item.itemType is ItemType.Instrument or ItemType.Light)
                slot.UpdateShelfPercentageUI(slot.item.maxTimeShelfLife, slot.item.currTimeShelfLife);
        }
    }
}
