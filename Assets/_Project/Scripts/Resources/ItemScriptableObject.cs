using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Resources.Visitor;
using UnityEngine;

public enum ItemType {Food, Instrument, Material, Light, BuildItem}
public abstract class ItemScriptableObject : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;
    public string itemName;
    public int maximumAmount;
    public Sprite icon;
    public float maxTimeShelfLife;
    public float currTimeShelfLife;
    public float burnLevel;
    public abstract void Accept(IItemVisitor visitor);
}
