using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{
    public Recipes recipes;
    private List<ItemData> m_requiredItems = new List<ItemData>();
    public PlayerInventoryHolder playerInventory;
    private int m_itemCounter;

    public void Craft(Recipes _recipe)
    {
        //Debug.Log(CheckForingredints(_recipe, out List<InventorySlot> invSlots, out List<ItemData> distinctItemList, out Dictionary<ItemData, int> itemAmounts));
        if (CheckForingredints(_recipe, out List<InventorySlot> _invSlots, out List<ItemData> _distinctItemList, out Dictionary<ItemData, int> _itemAmounts))
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
                playerInventory.AddToInventory(_recipe.outcome, _recipe.amountOfOutcome);
            }
            else if (_invSlots.Count > 1)
            {
                for (int i = 0; i < _invSlots.Count; i++)
                {
                    if (_invSlots[i].StackSize > _itemAmounts[_invSlots[i].ItemData])
                    {
                        int itemsLeftInStack = _invSlots[i].StackSize - _itemAmounts[_invSlots[i].ItemData];
                        _invSlots[i].UpdateInventorySlot(_invSlots[i].ItemData, itemsLeftInStack);
                        PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                    }
                    else if (_invSlots[i].StackSize <= _itemAmounts[_invSlots[i].ItemData])
                    {
                        _itemAmounts[_invSlots[i].ItemData] = _itemAmounts[_invSlots[i].ItemData] - _invSlots[i].StackSize;
                        _invSlots[i].ClearSlot();
                        PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                    }
                }
                playerInventory.AddToInventory(_recipe.outcome, _recipe.amountOfOutcome);
            }

        }


    }

    public bool TryRepair(List<ItemData> _itemsNeedToRepair)
    {
        //Debug.Log(CheckForingredints(_recipe, out List<InventorySlot> invSlots, out List<ItemData> distinctItemList, out Dictionary<ItemData, int> itemAmounts));
        if (CheckForItemsinInventory(_itemsNeedToRepair, out List<InventorySlot> _invSlots, out List<ItemData> _distinctItemList, out Dictionary<ItemData, int> _itemAmounts))
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
            }
            else if (_invSlots.Count > 1)
            {
                for (int i = 0; i < _invSlots.Count; i++)
                {
                    if (_invSlots[i].StackSize > _itemAmounts[_invSlots[i].ItemData])
                    {
                        int itemsLeftInStack = _invSlots[i].StackSize - _itemAmounts[_invSlots[i].ItemData];
                        _invSlots[i].UpdateInventorySlot(_invSlots[i].ItemData, itemsLeftInStack);
                        PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                    }
                    else if (_invSlots[i].StackSize <= _itemAmounts[_invSlots[i].ItemData])
                    {
                        _itemAmounts[_invSlots[i].ItemData] = _itemAmounts[_invSlots[i].ItemData] - _invSlots[i].StackSize;
                        _invSlots[i].ClearSlot();
                        PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
                    }
                }
            }
            return true;

        }
        else return false;


    }

    public bool CheckForingredints(Recipes _recipeToCheck, out List<InventorySlot> _invSlot, out List<ItemData> _distinctItemList, out Dictionary<ItemData, int> _itemAmounts)
    {
        _distinctItemList = _recipeToCheck.recipe.Distinct<ItemData>().ToList();
        _itemAmounts = new();
        _invSlot = new();
        Dictionary<ItemData, int> checker = new();

        foreach (ItemData item in _distinctItemList)
        {
            //Debug.Log($"added {GetNumOfItem(_recipeToCheck.recipe, item)} {item} to _itemAmounts");
            _itemAmounts.Add(item, GetNumOfItem(_recipeToCheck.recipe, item));
        }
        foreach (ItemData item in _distinctItemList)
        {
            checker.Add(item, 0);
            for (int x = playerInventory.PrimaryInventorySystem.inventorySize - 1; x > -1; x--)
            {
                if (checker[item] < _itemAmounts[item])
                {
                    if (playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData == item)
                    {
                        _invSlot.Add(playerInventory.PrimaryInventorySystem.InventorySlots[x]);
                        checker[item] += playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize;
                        //Debug.Log($"the checker has {checker[item]} {item} and you need {_itemAmounts[item]} {item}");
                    }
                }

            }
            if (checker[item] < _itemAmounts[item])
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckForItemsinInventory(List<ItemData> _itemsToCheckFor, out List<InventorySlot> _invSlot, out List<ItemData> _distinctItemList, out Dictionary<ItemData, int> _itemAmounts)
    {
        _distinctItemList = _itemsToCheckFor.Distinct<ItemData>().ToList();
        _itemAmounts = new();
        _invSlot = new();
        Dictionary<ItemData, int> checker = new();

        foreach (ItemData item in _distinctItemList)
        {
            Debug.Log($"added {GetNumOfItem(_itemsToCheckFor, item)} {item} to _itemAmounts");
            _itemAmounts.Add(item, GetNumOfItem(_itemsToCheckFor, item));
        }
        foreach (ItemData item in _distinctItemList)
        {
            checker.Add(item, 0);
            for (int x = playerInventory.PrimaryInventorySystem.inventorySize - 1; x > -1; x--)
            {
                if (checker[item] < _itemAmounts[item])
                {
                    if (playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData == item)
                    {
                        _invSlot.Add(playerInventory.PrimaryInventorySystem.InventorySlots[x]);
                        checker[item] += playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize;
                        Debug.Log($"the checker has {checker[item]} {item} and you need {_itemAmounts[item]} {item}");
                    }
                }

            }
            if (checker[item] < _itemAmounts[item])
            {
                return false;
            }
        }
        return true;
    }

    public int GetNumOfItem(List<ItemData> _list, ItemData _itemToCount)
    {
        int count = 0;
        List<ItemData> distinctItemList = _list.Distinct<ItemData>().ToList();
        foreach (ItemData item in distinctItemList)
        {
            foreach (ItemData _item in _list)
            {
                if (_item == item && _item == _itemToCount)
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