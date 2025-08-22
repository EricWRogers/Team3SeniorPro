using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]

public class InventorySystem
{
    //This script manages the inventory logic
    [SerializeField] private List<InventorySlot> m_inventorySlots;

    public List<InventorySlot> inventorySlots => m_inventorySlots;
    public int inventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int _size) // creates the slots for inventory
    {
        m_inventorySlots = new List<InventorySlot>(_size);

        for (int x = 0; x < _size; x++)
        {
            m_inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(ItemData _itemToAdd, int _amountToAdd) //adds item to a stack of like items or empty slot if open
    {
        if (ContainsItem(_itemToAdd, out List<InventorySlot> invSlot))//check for item in inventory
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(_amountToAdd))
                {
                    slot.AddToStack(_amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeSlot))//check for free slot in inventory
        {
            freeSlot.UpdateInventorySlot(_itemToAdd, _amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(ItemData _itemToAdd, out List<InventorySlot> _invSlot)//check for same item
    {
        _invSlot = inventorySlots.Where(i => i.itemData == _itemToAdd).ToList();

        return _invSlot == null ? false : true;

    }

    public bool HasFreeSlot(out InventorySlot _freeSlot)//check for free slot
    {
        _freeSlot = inventorySlots.FirstOrDefault(i => i.itemData == null);
        return _freeSlot == null ? false : true;

    }

    
}
