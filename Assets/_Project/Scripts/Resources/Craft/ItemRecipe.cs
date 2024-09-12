using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Recipe",menuName = "Scriptable Object/Item Recipe")]
public class ItemRecipe : ScriptableObject
{
    public string recipeName;
    public ItemTypeAndCount[] Input;
    public ItemTypeAndCount Otput;
}

[System.Serializable]
public class ItemTypeAndCount
{
    public ItemScriptableObject item;
    public int count;

    public ItemTypeAndCount(ItemScriptableObject i, int c)
    {
        item = i;
        count = c;
    }

}
