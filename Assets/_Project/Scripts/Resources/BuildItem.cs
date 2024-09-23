using System.Collections;
using System.Collections.Generic;
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
}
