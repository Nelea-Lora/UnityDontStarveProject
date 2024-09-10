using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Food, Instrument, Material, CookedFood}
public class ItemScriptableObject : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;
    public string itemName;
    public int maximumAmount;
    public string itemDescription;
    public Sprite icon;
}
