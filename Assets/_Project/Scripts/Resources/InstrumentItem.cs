using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Instrument Item",menuName = "Inventory/Items/New Instrument Item")]
public class InstrumentItem : ItemScriptableObject
{
    public float forceAmount;
    public InstrumentType instrumentType;
    private void Start()
    {
        itemType = ItemType.Instrument;
    } 
}
