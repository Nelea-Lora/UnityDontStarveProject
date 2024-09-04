using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlotDragAndDrop : MonoBehaviour//, IDropHandler
{
    private InventorySlot _newSlot;
    void Start()
    {
        _newSlot = transform.GetComponent<InventorySlot>();
    }
    // public void OnDrop(PointerEventData eventData)
    // {
    //     if(!_newSlot.isComplete)
    //     {
    //         var otherItemTransform = eventData.pointerDrag.transform;
    //         otherItemTransform.SetParent(transform);
    //         otherItemTransform.localPosition = Vector3.zero;
    //         print("_newSlot " + _newSlot);
    //         _newSlot.SlotComplete();
    //     }
    // }
}
