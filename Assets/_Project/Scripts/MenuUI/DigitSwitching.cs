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
    private Sprite _image;
    
    void Update()
    {
        for(int i = 0; i < slotParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) {
                if (currentSlotID == i)
                {
                    _image = slotParent.GetChild(currentSlotID).GetComponent<Image>().sprite;
                    if (_image == notSelectedImage) _image = selectedImage;
                    else _image = notSelectedImage;
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
