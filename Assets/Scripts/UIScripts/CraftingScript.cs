using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{
    public Recipes recipes;
    public List<ItemData> m_requiredItems = new List<ItemData>();
    public PlayerInventoryHolder playerInventory;

    public bool hasAllIngredints;

    public void Craft(RecipeRef _recipe)
    {
        Debug.Log(CheckForingredints(_recipe, out List<InventorySlot> _invSlot));


    }

    public bool CheckForingredints(RecipeRef _recipeToCheck, out List<InventorySlot> _invSlot)
    {
        m_requiredItems = (recipes.allRecipes[_recipeToCheck.recipe]);
        List<ItemData> distinctItemList = m_requiredItems.Distinct<ItemData>().ToList();
        PseudoDictionary<ItemData, int> requiredItemDict = new PseudoDictionary<ItemData, int>();
        _invSlot = new();

        foreach (ItemData item in distinctItemList)
        {
            requiredItemDict.Add(item, GetNumOfItem(m_requiredItems, item));
        }
        foreach (ItemData item in distinctItemList)
        {
            int itemCounter = requiredItemDict[item];
            for (int x = playerInventory.PrimaryInventorySystem.inventorySize - 1; x > -1; x--)
            {
                if (playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData == item && itemCounter >= 0)
                {
                    _invSlot.Add(playerInventory.PrimaryInventorySystem.InventorySlots[x]);
                    itemCounter -= playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize;
                    //Debug.Log($"inventory slot {x} has {playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize} {playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData} ");
                }
                
            }
            if (itemCounter > 0)
            {
                return false;
            }
        }
        return true;
    }

    public int GetNumOfItem<ItemData>(List<ItemData> _list, ItemData _itemToCount)
    {
        int count = 0;
        List<ItemData> distinctItemList = _list.Distinct<ItemData>().ToList();
        foreach (ItemData item in distinctItemList)
        {
            foreach (ItemData _item in _list)
            {
                if (EqualityComparer<ItemData>.Default.Equals(_item, _itemToCount))
                {
                    count++;
                }
            }
        }
            
        return count;
    }
}
