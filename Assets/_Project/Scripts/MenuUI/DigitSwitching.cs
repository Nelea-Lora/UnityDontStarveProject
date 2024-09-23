using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DigitSwitching : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
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
        for(int i = 0; i < slotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) {
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
                    //if(_playerController.itemInHands)_playerController.ItIsAnotherObjectInHand();
                    slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = notSelectedImage;
                    currentSlotID = i;
                    slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = selectedImage;
                    TakeItemInHands();
                    tmpCurrentSlotID = currentSlotID;
                }
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
}
