using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Item",menuName = "Inventory/Items/New Food Item")]
public class FoodItem : ItemScriptableObject
{
    public float healingAmount;
    public float eatingAmount;
    public float mindAmount;
    private void Start()
    {
        itemType = ItemType.Food;
    } 
}
