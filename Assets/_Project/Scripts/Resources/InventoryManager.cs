using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform _inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private Camera _mainCamera;
    private bool _entered;
    private Item _itemTmp;

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.E) && _entered && _itemTmp)
        {
            GetItem(_itemTmp);
        }
    }

    private void GetItem(Item item)
    {
        AddItem(item.gameObject.GetComponent<Item>()._Item,
            item.gameObject.GetComponent<Item>()._amount);
        Destroy(item.gameObject);
        _entered = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            _entered = true;
            _itemTmp = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            _entered = false;
            _itemTmp = null;
        }
    }

    private void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                slot.amount += _amount;
                slot.itemAmount.text = slot.amount.ToString();
                return;
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
