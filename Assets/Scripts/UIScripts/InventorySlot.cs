using UnityEngine;

[System.Serializable]

public class InventorySlot
{
    //this is the logic behind each inventory slot
    [SerializeField] private ItemData m_itemData;
    [SerializeField] private int m_stackSize;

    public ItemData itemData => m_itemData;
    public int stackSize => m_stackSize;

    public InventorySlot(ItemData _source, int _amount)
    {
        m_itemData = _source;
        m_stackSize = _amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        m_itemData = null;
        m_stackSize = -1;
    }

    public void UpdateInventorySlot(ItemData _itemData, int _amount)
    {
        m_itemData = _itemData;
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
}
