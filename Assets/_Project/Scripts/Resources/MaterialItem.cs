using _Project.Scripts.Resources.Visitor;
using UnityEngine;


[CreateAssetMenu(fileName = "Material Item",menuName = "Inventory/Items/New Material Item")]
public class MaterialItem : ItemScriptableObject
{
    private void Start()
    {
        itemType = ItemType.Material;
    }
    public override void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }

}
