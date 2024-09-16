using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private void Start()
    {
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!oldSlot&&!oldSlot.isComplete) return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!oldSlot.isComplete) return;
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!oldSlot.isComplete) return;
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        GetComponentInChildren<Image>().raycastTarget = true;
        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            InventorySlot newSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.
                GetComponent<InventorySlot>();
            if (newSlot != null)
            {
                ExchangeSlotData(newSlot);
            }
        }
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject item = newSlot.item;
        int amount = newSlot.amount;
        bool isComplete = newSlot.isComplete;
        Sprite iconGo = newSlot.iconGO.sprite;
        
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isComplete)
        {
            newSlot.SetIcon(oldSlot.iconGO.sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.color = new Color(1, 1, 1, 0);
            newSlot.iconGO.sprite = null;
            newSlot.itemAmount.text = "";
        }
        newSlot.isComplete = oldSlot.isComplete;
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isComplete)
        { 
            oldSlot.SetIcon(iconGo);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else
        {
            oldSlot.iconGO.color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.sprite = null;
            oldSlot.itemAmount.text = "";
        }
        oldSlot.isComplete = isComplete;
    }
}
