using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CreateAssetMenu(fileName = "Light Item",menuName = "Inventory/Items/New Light Item")]
public class LightItem : ItemScriptableObject
{
    public PointLight Light;
    private void Start()
    {
        itemType = ItemType.Light;
    } 
}
