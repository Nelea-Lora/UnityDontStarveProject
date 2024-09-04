using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DigitSwitching : MonoBehaviour
{
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
                        print("_image == notSelectedImage");
                        slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite = selectedImage;
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
                }
            }
        }
    }
}
