using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class CraftManager : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private ItemRecipe _item;
    [SerializeField] private CraftPanel _craftPanel;
    [SerializeField] private TMP_Text _itemName;
    private bool _emptySlot;

    void Start()
    {
        _itemName.text = _item.Otput.item.itemName;
    }

    void Update()
    {
        if (_craftPanel.Сreate)
        {
            _craftPanel.ItemCreated();
            CraftInstrument();
        }
    }

    private void CraftInstrument()
    {
        if (!_item || _item.Input is null) return;
        if (_inventoryManager.slots is null) return;
        int allMaterials = _item.Input.Length;
        foreach (ItemTypeAndCount material in _item.Input)
        {
            foreach (InventorySlot slot in _inventoryManager.slots)
            {
                VerifySlots();
                if (slot.item && slot.item == material.item && slot.amount>=material.count)
                {
                    if (slot.amount == material.count) _emptySlot = true;
                    allMaterials--;
                    break;
                }
            }
        }
        print("allMaterials" + allMaterials);
        if (!_emptySlot) return;
        if (allMaterials == 0) CreateItem();
    }

    private void CreateItem()
    {
        print("CreateItem()");
        foreach (ItemTypeAndCount material in _item.Input)
        {
            foreach (InventorySlot slot in _inventoryManager.slots)
            {
                if (slot.item == material.item)
                {
                    print("slot.item " + slot.item);
                    print("slot.amount " + slot.amount);
                    if (slot.amount <= material.count) slot.NullifySlotData();
                    else slot.DecreaseSlotData(material.count);
                    print("slot.amount " + slot.amount);
                    break;
                }
            }
        }
        _inventoryManager.AddItem(_item.Otput.item, _item.Otput.count);
        _emptySlot = false;
    }
    public void VerifySlots()
    {
        foreach (InventorySlot slot in _inventoryManager.slots)
        {
            if (!slot.isComplete) _emptySlot = true;
        }
    }
}