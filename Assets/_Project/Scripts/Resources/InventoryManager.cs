using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform _inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private Camera _mainCamera;
    public float reachDistance = 30f;

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
        print("InventoryManager");
    }

    void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            if (hit.collider.gameObject.GetComponent<Item>() && Input.GetKeyDown(KeyCode.E))
            {
                AddItem(hit.collider.gameObject.GetComponent<Item>()._Item,
                    hit.collider.gameObject.GetComponent<Item>()._amount);
                Destroy(hit.collider.gameObject);
            }
        }
        else { Debug.DrawRay(ray.origin, ray.direction, Color.red);}
    }

    private void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                slot.amount += _amount;
                print("slot.item == _item / _amount = " + _amount);
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
                print("!slot.isComplete / _amount = " + _amount);
                slot.itemAmount.text = slot.amount.ToString();
                break;
            }
        }
    }
}
