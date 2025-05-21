using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Resources.Decorator;
using _Project.Scripts.Resources.Visitor;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Item",menuName = "Inventory/Items/New Food Item")]
public class FoodItem : ItemScriptableObject
{
    public float healingAmount;
    public float eatingAmount;
    public float mindAmount;
    public FoodEffect foodEffect;
    private void Start()
    {
        itemType = ItemType.Food;
    } 
    public override void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }
    
    public void ApplyEffect()
    {
        foodEffect?.ApplyEffect(this);
    }
    
}
