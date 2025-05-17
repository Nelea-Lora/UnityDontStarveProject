using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Resources.Visitor;
using UnityEngine;

[CreateAssetMenu(fileName = "Build Item",menuName = "Inventory/Items/New Build Item")]
public class BuildItem : ItemScriptableObject
{
    public ItemOnSceneType buildItemType;
    public InstrumentType instrumentType;
    private void Start()
    {
        itemType = ItemType.BuildItem;
    } 
    public override void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
