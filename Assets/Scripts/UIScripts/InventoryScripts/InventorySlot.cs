using System;
using UnityEngine;

[System.Serializable]

public class InventorySlot : ISerializationCallbackReceiver
{
    //this is the logic behind each inventory slot
    [NonSerialized] private ItemData m_itemData;
    [SerializeField] private int m_itemID = -1;
    [SerializeField] private int m_stackSize;

    public ItemData ItemData => m_itemData;
    public int StackSize => m_stackSize;

    public InventorySlot(ItemData _source, int _amount)
    {
        m_itemData = _source;
        m_itemID = m_itemData.id;
        m_stackSize = _amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void AssignItem(InventorySlot _invSlot)
    {
        if (ItemData == _invSlot.ItemData)
        {
            AddToStack(_invSlot.StackSize);
        }
        else
        {
            m_itemData = _invSlot.ItemData;
            m_itemID = m_itemData.id;
            m_stackSize = 0;
            AddToStack(_invSlot.StackSize);
        }
    }

    public void ClearSlot()
    {
        m_itemData = null;
        m_itemID = -1;
        m_stackSize = -1;
    }

    public void UpdateInventorySlot(ItemData _itemData, int _amount)
    {
        m_itemData = _itemData;
        m_itemID = m_itemData.id;
        m_stackSize = _amount;
    }

    public bool RoomLeftInStack(int _amountToAdd, out int _amountRemaining)
    {
        _amountRemaining = m_itemData.maxStack - m_stackSize;

        return RoomLeftInStack(_amountToAdd);
    }

    public bool RoomLeftInStack(int _amountToAdd)
    {
        if (m_stackSize + _amountToAdd <= m_itemData.maxStack) return true;
        else return false;
    }

    public void AddToStack(int _amount)
    {
        m_stackSize += _amount;
    }
    public void RemoveFromStack(int _amount)
    {
        m_stackSize -= _amount;
    }

    public bool SplitStack(out InventorySlot _splitStack)
    {
        if (StackSize <= 1)
        {
            _splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(StackSize / 2);
        RemoveFromStack(halfStack);

        _splitStack = new InventorySlot(ItemData, halfStack);
        return true;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (m_itemID == -1) return;

        var db = Resources.Load<Database>("ItemDatabase");
        m_itemData = db.GetItem(m_itemID);
    }
}
