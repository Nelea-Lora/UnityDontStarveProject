using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DragAndDropItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private UISlotDragAndDrop _slotDrop;
    public InventorySlot oldSlot;
    public bool currentSlotCompleted;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        slotTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        InventorySlot newSlot = eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>();
        if (newSlot != null && currentSlotCompleted)
        {
            print("old slot  " + oldSlot);
            oldSlot.SlotEmpty();
            oldSlot = newSlot;
            currentSlotCompleted = false;
        }
    }
}

// public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
// {
//     public InventorySlot oldSlot;
//     private Transform player;
//     private InventoryManager inventoryManager;
//     private void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//         oldSlot = transform.GetComponentInParent<InventorySlot>();
//         inventoryManager = FindObjectOfType<InventoryManager>();
//     }
//     public void OnDrag(PointerEventData eventData)
//     {
//         if (!oldSlot.isComplete) return;
//         GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
//     }
//     public void OnPointerDown(PointerEventData eventData)
//     {
//         if (!oldSlot.isComplete) return;
//         GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
//         GetComponentInChildren<Image>().raycastTarget = false;
//         transform.SetParent(transform.parent.parent);
//     }
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         if (!oldSlot.isComplete) return;
//         GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
//         GetComponentInChildren<Image>().raycastTarget = true;
//         transform.SetParent(oldSlot.transform);
//         print(" transform.SetParent(oldSlot.transform) = "+oldSlot);
//         transform.position = oldSlot.transform.position;
//         if (eventData.pointerCurrentRaycast.gameObject != null)
//         {
//             InventorySlot newSlot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.
//                 GetComponent<InventorySlot>();
//             print("OnPointerUp   newSlot "+newSlot);
//             if (newSlot != null)
//             {
//                 ExchangeSlotData(newSlot);
//                 NullifySlotData();
//             }
//         }
//     }
//     void NullifySlotData()
//     {
//         oldSlot.item = null;
//         oldSlot.amount = 0;
//         oldSlot.isComplete = false;
//         oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
//         oldSlot.iconGO.GetComponent<Image>().sprite = null;
//         oldSlot.itemAmount.text = "";
//     }
//     void ExchangeSlotData(InventorySlot newSlot)
//     {
//         print(" newSlot "+newSlot);
//         print(" oldSlot "+oldSlot);
//         ItemScriptableObject item = newSlot.item;
//         int amount = newSlot.amount;
//         bool isComplete = newSlot.isComplete;
//         GameObject iconGO = newSlot.iconGO;
//
//         newSlot.item = oldSlot.item;
//         newSlot.amount = oldSlot.amount;
//         if (oldSlot.isComplete)
//         {
//             newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
//             newSlot.itemAmount.text = oldSlot.amount.ToString();
//         }
//         else
//         {
//             newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
//             newSlot.iconGO.GetComponent<Image>().sprite = null;
//             newSlot.itemAmount.text = "";
//         }
//         newSlot.isComplete = oldSlot.isComplete;
//         oldSlot.item = item;
//         oldSlot.amount = amount;
//         if (isComplete)
//         {
//             oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
//             oldSlot.itemAmount.text = amount.ToString();
//         }
//         else
//         {
//             oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
//             oldSlot.iconGO.GetComponent<Image>().sprite = null;
//             oldSlot.itemAmount.text = "";
//         }
//         oldSlot.isComplete = isComplete;
//     }
// }
