using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]

public class InventorySystem
{
    //This script manages the inventory logic
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int inventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int _size) // creates the slots for inventory
    {
        inventorySlots = new List<InventorySlot>(_size);

        for (int x = 0; x < _size; x++)
        {
            inventorySlots.Add(new InventorySlot());
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
            if (_itemToAdd.maxStack < _amountToAdd)
            {
                int amountOfFullStacks = _amountToAdd / _itemToAdd.maxStack;
                int itemsLeftToAdd = _amountToAdd - (_itemToAdd.maxStack * amountOfFullStacks);
                for (int i = 0; i < amountOfFullStacks; i++)
                {
                    if (HasFreeSlot(out InventorySlot freeSlot1))
                    {
                        freeSlot1.UpdateInventorySlot(_itemToAdd, _itemToAdd.maxStack);
                        OnInventorySlotChanged?.Invoke(freeSlot1);
                    }
                    
                }
                AddToInventory(_itemToAdd, itemsLeftToAdd);
            }
            else
            {
                freeSlot.UpdateInventorySlot(_itemToAdd, _amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
            
        }

        return false;
    }

    public bool ContainsItem(ItemData _itemToAdd, out List<InventorySlot> _invSlot)//check for same item
    {
        _invSlot = InventorySlots.Where(i => i.ItemData == _itemToAdd).ToList();

        return _invSlot == null ? false : true;

    }

    public bool HasFreeSlot(out InventorySlot _freeSlot)//check for free slot
    {
        _freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return _freeSlot == null ? false : true;

    }

    
}
