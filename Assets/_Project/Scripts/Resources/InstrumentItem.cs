using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Resources.Visitor;
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
    public override void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

}
