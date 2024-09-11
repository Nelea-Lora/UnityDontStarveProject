using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class CraftManager : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;

    //[SerializeField] private ItemScriptableObject _item;
    private CraftSlot _craftSlot;
    private ItemScriptableObject _item;
    private List<ItemScriptableObject> materialsToUse = new List<ItemScriptableObject>();
    [SerializeField] private CraftPanel _craftPanel;
    private bool _emptySlot;

    void Start()
    {
        _craftSlot = GetComponentInParent<CraftSlot>();
        _item = _craftSlot.item;
    }

    void Update()
    {
        if (_item.itemType == ItemType.Instrument && _craftPanel.Сreate)
        {
            _craftPanel.ItemCreated();
            CraftInstrument();
        }
    }

    private void CraftInstrument()
    {
        if (!_item) return;
        if (_inventoryManager.slots is null) return;
        InstrumentItem instrument = _item as InstrumentItem;
        if (!instrument) return;
        int allMaterials = instrument.materials.Count;
        foreach (ItemScriptableObject material in instrument.materials)
        {
            foreach (InventorySlot slot in _inventoryManager.slots)
            {
                VerifySlots();
                if (slot.item && slot.item == material)
                {
                    if (slot.amount == 1) _emptySlot = true;
                    materialsToUse.Add(slot.item);
                    allMaterials--;
                    break;
                }
            }
        }
        print("allMaterials" + allMaterials);
        if (!_emptySlot) return;
        if (allMaterials == 0 && materialsToUse is not null) CreateItem();
    }

    private void CreateItem()
    {
        //if (materialsToUse is null) return;
        print("CreateItem()");
        for (int i = 0; i < materialsToUse.Count; i++)
        {
            foreach (InventorySlot slot in _inventoryManager.slots)
            {
                if (slot.item == materialsToUse[i])
                {
                    print("slot.item " + slot.item);
                    print("slot.amount " + slot.amount);
                    if (slot.amount <= 1) slot.NullifySlotData();
                    else slot.DecreaseSlotData();
                    print("slot.amount " + slot.amount);
                    break;
                }
            }
        }
        materialsToUse.Clear();
        _inventoryManager.AddItem(_item, 1);
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