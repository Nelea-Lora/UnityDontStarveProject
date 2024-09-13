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
    private int _tmpCurrentSlotID ;
    [SerializeField]private Sprite selectedImage;
    [SerializeField]private Sprite notSelectedImage;
    public Transform slotParent;

    private void Start()
    {
        _tmpCurrentSlotID = slotParent.childCount;
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
                        _tmpCurrentSlotID = currentSlotID;
                    }
                    // else
                    // {
                    //     print("_image == selectedImage");
                    //     slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = notSelectedImage;
                    // }
                }
                else
                {
                    if(_playerController.itemInHands)_playerController.ItIsAnotherObjectInHand();
                    slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = notSelectedImage;
                    currentSlotID = i;
                    slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = selectedImage;
                    TakeItemInHands();
                    _tmpCurrentSlotID = currentSlotID;
                }
            }
        }
    }
    private void TakeItemInHands()
    {
        if (!_playerController) return;
        InventorySlot currentItem = slotParent.GetChild(currentSlotID)
            .GetComponent<InventorySlot>();
        if (!currentItem || !currentItem.item || !currentItem.itemAmount || !currentItem.iconGO
            || currentItem.amount <= 0) return;
        _playerController.itemInHands = currentItem.item;
        if(_playerController.itemInHands &&_tmpCurrentSlotID != currentSlotID)
            _playerController.TakeObjectInRightHand();
    }
}
