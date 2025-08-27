using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepairScript : MonoBehaviour
{
    private PlayerInventoryHolder m_playerInventory;
    private int m_itemCounter;

    void Awake()
    {
        m_playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventoryHolder>();
    }
    public bool TryRepair(List<ItemData> _itemsNeedToRepair)
    {
        Debug.Log("try to repair");
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
            for (int x = m_playerInventory.PrimaryInventorySystem.inventorySize - 1; x > -1; x--)
            {
                Debug.Log($"inventory slot item {m_playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData} with {m_playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize}");
                if (checker[item] < _itemAmounts[item])
                {
                    //Debug.Log($"checker has {checker[item]} {item} and itemAmounts has {_itemAmounts[item]} {item} {m_playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData.id} {item.id}");
                    if (m_playerInventory.PrimaryInventorySystem.InventorySlots[x].ItemData == item)
                    {
                        Debug.Log("1");
                        _invSlot.Add(m_playerInventory.PrimaryInventorySystem.InventorySlots[x]);
                        checker[item] += m_playerInventory.PrimaryInventorySystem.InventorySlots[x].StackSize;
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
