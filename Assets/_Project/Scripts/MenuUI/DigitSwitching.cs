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
    [SerializeField]private Sprite selectedImage;
    [SerializeField]private Sprite notSelectedImage;
    public Transform slotParent;
    
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
                    }
                    // else
                    // {
                    //     print("_image == selectedImage");
                    //     slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = notSelectedImage;
                    // }
                }
                else
                {
                    slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = notSelectedImage;
                    currentSlotID = i;
                    slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = selectedImage;
                    TakeItemInHands();
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
        print("itemInHands " + _playerController.itemInHands);
    }
}
