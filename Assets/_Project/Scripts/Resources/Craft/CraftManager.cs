using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftManager : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    //[SerializeField] private ItemScriptableObject _item;
    private CraftSlot _craftSlot;
    private ItemScriptableObject _item;
    private List<ItemScriptableObject> materialsToUse = new List<ItemScriptableObject>();
    [SerializeField] private CraftPanel _craftPanel;
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

    public void CraftInstrument()
    {
        if(!_item)return;
        if(_inventoryManager.slots is null)return;
        InstrumentItem instrument = _item as InstrumentItem;
        if (!instrument) return;
        int allMaterials = instrument.materials.Count;
        foreach (ItemScriptableObject material in instrument.materials)
        {
            foreach (InventorySlot slot in _inventoryManager.slots)
            {
                if (slot.item == material)
                {
                    materialsToUse.Add(slot.item);
                    allMaterials--;
                    break;
                }
            }
        }
        if (allMaterials > 0) return;
        else if (allMaterials == 0)
        {
            foreach (InventorySlot slot in _inventoryManager.slots)
            {
                for (int i = 0; i < materialsToUse.Count; i++)
                {
                    if (slot.item == materialsToUse[i])
                    {
                        slot.DecreaseSlotData();
                        if(slot.amount<=0)slot.NullifySlotData();
                    }
                }
            }
            _inventoryManager.AddItem(_item,1);
        }
    }
}
