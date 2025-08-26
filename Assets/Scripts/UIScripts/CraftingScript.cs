using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{
    public Recipes recipes;
    public List<ItemData> m_requiredItems = new List<ItemData>();
    public PlayerInventoryHolder playerInventory;
    private int m_itemCounter;

    public void Craft(RecipeRef _recipe)
    {
        if (CheckForingredints(_recipe, out List<InventorySlot> _invSlots))
        {
            if (_invSlots.Count == 1)
            {
                if (m_itemCounter < 0)
                {
                    int itemsLeftInStack = Mathf.Abs(m_itemCounter);
                    _invSlots[0].UpdateInventorySlot(_invSlots[0].ItemData, itemsLeftInStack);
                    PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                }
                else if (m_itemCounter == 0)
                {
                    _invSlots[0].ClearSlot();
                    PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                }
                playerInventory.AddToInventory(_recipe.recipe, 1);
            }

        }


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
            m_itemCounter = requiredItemDict[item];
            for (int x = playerInventory.PrimaryInventorySystem.inventorySize - 1; x > -1; x--)
            {
                if (m_itemCounter > 0)
                {
                    if (playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData == item)
                    {
                        _invSlot.Add(playerInventory.PrimaryInventorySystem.InventorySlots[x]);
                        m_itemCounter -= playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize;
                        Debug.Log($"inventory slot {x} has {playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize} {playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData} ");
                        Debug.Log($"itemCounter is {m_itemCounter}");
                    }
                }

            }
            if (m_itemCounter > 0)
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

    public Dictionary<ItemData, int> MakeDistinctDict(List<ItemData> _keys)
    {
        Dictionary<ItemData, int> _distinctDict = new();
        foreach (ItemData item in _keys)
        {
            _distinctDict.Add(item, GetNumOfItem(_keys, item));
        }
        return _distinctDict;
    }
}