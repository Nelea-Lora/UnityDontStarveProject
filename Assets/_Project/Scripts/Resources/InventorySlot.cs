using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public Image iconGO;
    public TMP_Text itemAmount;
    internal bool isComplete { get; set; }

    public void SlotComplete()
    {
        isComplete = true;
    }
    public void SlotEmpty()
    {
        isComplete = false;
    }
    
    public void SetIcon(Sprite icon)
    {
        iconGO.color = new Color(1, 1, 1, 1);
        iconGO.sprite = icon;

    }
    public void NullifySlotData()
    {
        item = null;
        amount = 0;
        isComplete = false;
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        iconGO.GetComponent<Image>().sprite = null;
        itemAmount.text = "";
    }
    public void DecreaseSlotData()
    {
        amount -=1;
        itemAmount.text = amount.ToString();
    }
    
}
