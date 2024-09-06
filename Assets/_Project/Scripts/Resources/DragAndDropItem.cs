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
// public class DragAndDropItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
// {
//     private CanvasGroup _canvasGroup;
//     private RectTransform _rectTransform;
//     private UISlotDragAndDrop _slotDrop;
//     public InventorySlot oldSlot;
//     public bool currentSlotCompleted;
//     private int _originalIndex;
//
//     void Start()
//     {
//         _rectTransform = GetComponent<RectTransform>();
//         _canvasGroup = GetComponent<CanvasGroup>();
//         oldSlot = transform.GetComponentInParent<InventorySlot>();
//     }
//
//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         var slotTransform = _rectTransform.parent;
//         _originalIndex = transform.GetSiblingIndex();
//         slotTransform.SetAsLastSibling();
//         _canvasGroup.blocksRaycasts = false;
//     }
//     public void OnDrag(PointerEventData eventData)
//     {
//         _rectTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
//     }
//     public void OnEndDrag(PointerEventData eventData)
//     {
//         print("_originalIndex  "+_originalIndex);
//         //transform.SetSiblingIndex(_originalIndex);
//         transform.localPosition = Vector3.zero;
//         _canvasGroup.blocksRaycasts = true;
//         InventorySlot newSlot = eventData.pointerDrag.transform.parent.GetComponent<InventorySlot>();
//         if (newSlot != null)
//         { 
//             print(" newSlot "+newSlot);
//          print(" oldSlot "+oldSlot);
//          // newSlot.transform.GetChild(0).SetParent(oldSlot.transform);
//          // newSlot.transform.GetChild(0).localPosition = transform.parent.localPosition;
//          ItemScriptableObject item = newSlot.item;
//          int amount = newSlot.amount;
//          bool isComplete = newSlot.isComplete;
//          GameObject iconGO = newSlot.iconGO;
//
//          newSlot.item = oldSlot.item;
//          newSlot.amount = oldSlot.amount;
//          if (oldSlot.isComplete)
//          {
//              newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
//              newSlot.itemAmount.text = oldSlot.amount.ToString();
//          }
//          else
//          {
//              newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
//              newSlot.iconGO.GetComponent<Image>().sprite = null;
//              newSlot.itemAmount.text = "";
//          }
//          newSlot.isComplete = oldSlot.isComplete;
//          oldSlot.item = null;
//          oldSlot.amount = 0;
//          oldSlot.isComplete = false;
//          oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
//          oldSlot.iconGO.GetComponent<Image>().sprite = null;
//          oldSlot.itemAmount.text = "";
//          //oldSlot = newSlot;
//             // print("old slot  " + oldSlot);
//             // //newSlot.transform.GetChild(0).SetParent(oldSlot.transform);
//             // //print("newSlot.transform.GetChild(0)  "+newSlot+ "  "+ newSlot.transform.GetChild(0));
//             // oldSlot.SlotEmpty();
//             // oldSlot = newSlot;
//             // //currentSlotCompleted = false;
//         }
//     }
// }
