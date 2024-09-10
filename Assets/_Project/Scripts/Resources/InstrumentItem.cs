using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Instrument Item",menuName = "Inventory/Items/New Instrument Item")]
public class InstrumentItem : ItemScriptableObject
{
    public float forceAmount;
    public InstrumentType instrumentType;
    public List<ScriptableObject> materials = new List<ScriptableObject>();
    private void Start()
    {
        itemType = ItemType.Instrument;
    } 
}
