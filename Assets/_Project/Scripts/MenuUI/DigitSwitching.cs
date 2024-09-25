using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DigitSwitching : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameMenu _gameMenu;
    public int currentSlotID ;
    public int tmpCurrentSlotID ;
    [SerializeField]private Sprite selectedImage;
    [SerializeField]private Sprite notSelectedImage;
    public Transform slotParent;

    private void Start()
    {
        tmpCurrentSlotID = slotParent.childCount;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&_gameMenu.Select&&EventSystem.current.IsPointerOverGameObject())
        {
            print("_gameMenu.Select");
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            foreach (RaycastResult result in results)
            {
                InventorySlot inventorySlot = result.gameObject.GetComponent<InventorySlot>();
                if (inventorySlot != null)
                {
                    SelectSlot(inventorySlot.slotID); print("slotID "+inventorySlot.slotID);
                    break;
                }
            }
        }
        for(int i = 0; i < slotParent.childCount; i++)
        {
            if (Input.GetMouseButtonDown(0)) break;
            if (i<9 && Input.GetKeyDown((i + 1).ToString()))
            {
                SelectSlot(i);
            }
        }
    }

    public void TakeItemInHands()
    {
        if (!_playerController) return;
        InventorySlot currentItem = slotParent.GetChild(currentSlotID)
            .GetComponent<InventorySlot>();
        if(_playerController.itemInHands)_playerController.ItIsAnotherObjectInHand();
        if (!currentItem || !currentItem.item || !currentItem.itemAmount || !currentItem.iconGO
            || currentItem.amount <= 0) return;
        _playerController.itemInHands = currentItem.item;
        if (_playerController.itemInHands && tmpCurrentSlotID != currentSlotID)
            _playerController.TakeObjectInRightHand();
    }

    private void SelectSlot(int i)
    {
        if (currentSlotID == i)
        {
            if (slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite == notSelectedImage)
            {
                slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = selectedImage;
                TakeItemInHands();
                tmpCurrentSlotID = currentSlotID;
            }
            else if(!_playerController.itemInHands)
            {
                TakeItemInHands();
                tmpCurrentSlotID = currentSlotID;
            }
        }
        else
        {
            slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = notSelectedImage;
            currentSlotID = i;
            slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = selectedImage;
            TakeItemInHands();
            tmpCurrentSlotID = currentSlotID;
        }
    }
    
}
