using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialsToCraft : MonoBehaviour
{
    [SerializeField] private ItemRecipe _item;
    void Start()
    {
        if(!_item)return;
        int imageIndex;
        for (imageIndex = 0; imageIndex < _item.Input.Length; imageIndex++ )
        {
            print("MaterialsToCraft");
            TMP_Text textInChildren = transform.GetChild(imageIndex).GetComponentInChildren<TMP_Text>();
            if(!textInChildren)return;
            var componentInChildren = transform.GetChild(imageIndex).GetComponent<Image>();
            if(!componentInChildren)return;
            componentInChildren.sprite = _item.Input[imageIndex].item.icon;
            print("_item.Input[imageIndex].item"+_item.Input[imageIndex].item);
            textInChildren.text = _item.Input[imageIndex].count.ToString();
        }
    }
}
