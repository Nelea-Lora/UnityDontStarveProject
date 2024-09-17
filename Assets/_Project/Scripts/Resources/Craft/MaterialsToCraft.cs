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
            TMP_Text textInChildren = transform.GetChild(imageIndex).GetComponentInChildren<TMP_Text>();
            if(!textInChildren)return;
            var componentInChildren = transform.GetChild(imageIndex).GetComponent<Image>();
            if(!componentInChildren)return;
            componentInChildren.sprite = _item.Input[imageIndex].item.icon;
            textInChildren.text = _item.Input[imageIndex].count.ToString();
        }
    }
}
