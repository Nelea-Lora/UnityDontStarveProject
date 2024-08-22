using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private Items items;

    [SerializeField] private Transform _inventorySlots;

    private Slot[] slots;

    void Start()
    {
        items = _player.GetComponent<Items>();
        slots = _inventorySlots.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) 
        {
            bool active = false;
            if (items.hasItems[i])
            {
                active = true;
            }
            slots[i].UpdateSlot(active);
        }
    }
}
