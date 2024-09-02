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
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetItem();
        }
    }

    private void GetItem()
    {
        if (!_itemTmp) return;
        AddItem(_itemTmp._Item, _itemTmp._amount);
        Destroy(_itemTmp.gameObject);
    }
    void OnItemTriggerEnter(Item item)
    {
        _itemTmp = item;
    }

    private void OnItemTriggerExit(Item item)
    {
        _itemTmp = null;
    }

    private void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + _amount<=_item.maximumAmount)
                {
                    slot.amount += _amount;
                    slot.itemAmount.text = slot.amount.ToString();
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
                break;
            }
        }
    }
}
