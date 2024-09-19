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
    [SerializeField] private Slider _shelfLifeSlider;
    [SerializeField] private TMP_Text _shelfLifePercentage;
    //public int itemID;
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
        //itemID = 0;
        item = null;
        amount = 0;
        isComplete = false;
        iconGO.color = new Color(1, 1, 1, 0);
        iconGO.sprite = null;
        itemAmount.text = "";
        _shelfLifePercentage.text = "";
        _shelfLifeSlider.value = 0;
        // if(_playerController.itemInHands && item == _playerController.itemInHands)
        //     _playerController.ItIsAnotherObjectInHand();
    }
    public void DecreaseSlotData(int numToDecrease)
    {
        if(amount < numToDecrease)return;
        amount -=numToDecrease;
        itemAmount.text = amount.ToString();
        //if (item.maxTimeShelfLife>0)item.currTimeShelfLife = item.maxTimeShelfLife;
        if (amount <= 0) NullifySlotData();
    }

    public void ShelfMaxTime()
    {
        _shelfLifeSlider.maxValue = item.maxTimeShelfLife;
    }

    public void UpdateShelfSliderUI(float currentTime)
    {
        if (_shelfLifeSlider)
        {
            _shelfLifeSlider.value = currentTime;
        }
    }
    public void UpdateShelfPercentageUI(float maxTime, float currentTime)
    {
        if (_shelfLifePercentage)
        {
            int percentage = (int)((int)(currentTime * 100) / maxTime);
            if(percentage == 0)_shelfLifePercentage.text = "";
            else _shelfLifePercentage.text = percentage+"%";
        }
    }
    
}
