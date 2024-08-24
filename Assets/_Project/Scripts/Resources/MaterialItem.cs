using UnityEngine;


[CreateAssetMenu(fileName = "Material Item",menuName = "Inventory/Items/New Material Item")]
public class MaterialItem : ItemScriptableObject
{
    private void Start()
    {
        itemType = ItemType.Material;
    } 
}
