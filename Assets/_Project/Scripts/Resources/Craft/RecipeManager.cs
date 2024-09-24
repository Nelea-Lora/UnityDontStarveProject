using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private List<ItemRecipe> allRecipes;
    [SerializeField] private InventoryManager _inventoryManager;

    public void Cook(ItemScriptableObject itemToCook)
    {
        if(!itemToCook && itemToCook.itemName is null)return;
        ItemRecipe itemRecipe = FindRecipeByNameOfResult(itemToCook.itemName);
        if(itemRecipe && itemRecipe.Otput is not null && itemRecipe.Otput.item && itemRecipe.Otput.count>0)
            _inventoryManager.AddItem(itemRecipe.Otput.item,itemRecipe.Otput.count);
    }
    
    public ItemRecipe FindRecipeByNameOfResult(string recipeName)
    {
        if (allRecipes is null || recipeName is null) return null;
        foreach (var recipe in allRecipes)
        {
            // if (!recipe)
            // {
            //     print("***recipe do not exist");
            // }
            // if (recipe.Otput is null)
            // {
            //     print("///recipe.Otput is null");
            // }
            // if (!recipe.Otput.item)
            // {
            //     print("-----recipe.Otput.item do not exist");
            // }
            // if (recipe.Otput.item.itemName is null)
            // {
            //     print("++++recipe.Otput.item.itemName is null");
            // }
            if (!recipe || recipe.Otput is null || !recipe.Otput.item || recipe.Otput.item.itemName is null 
                || recipe.Otput.item.itemName is null)
            {
                print("recipe output do not exist");
                continue;
            }
            if (recipe.Otput.item.itemName == recipeName)
            {
                return recipe;
            }
        }
        return null;
    }
}
