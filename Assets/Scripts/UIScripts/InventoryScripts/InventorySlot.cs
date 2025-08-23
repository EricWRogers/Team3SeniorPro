using UnityEngine;

[System.Serializable]

public class InventorySlot
{
    //this is the logic behind each inventory slot
    [SerializeField] private ItemData itemData;
    [SerializeField] private int stackSize;

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData _source, int _amount)
    {
        itemData = _source;
        stackSize = _amount;
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
            itemData = _invSlot.ItemData;
            stackSize = 0;
            AddToStack(_invSlot.StackSize);
        }
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(ItemData _itemData, int _amount)
    {
        itemData = _itemData;
        stackSize = _amount;
    }

    public bool RoomLeftInStack(int _amountToAdd, out int _amountRemaining)
    {
        _amountRemaining = itemData.maxStack - stackSize;

        return RoomLeftInStack(_amountToAdd);
    }

    public bool RoomLeftInStack(int _amountToAdd)
    {
        if (stackSize + _amountToAdd <= itemData.maxStack) return true;
        else return false;
    }

    public void AddToStack(int _amount)
    {
        stackSize += _amount;
    }
    public void RemoveFromStack(int _amount)
    {
        stackSize -= _amount;
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
}
