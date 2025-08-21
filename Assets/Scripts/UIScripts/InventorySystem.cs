using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]

public class InventorySystem
{
    [SerializeField] private List<InventorySlot> m_inventorySlots;

    public List<InventorySlot> inventorySlots => m_inventorySlots;
    public int inventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int _size)
    {
        m_inventorySlots = new List<InventorySlot>(_size);

        for (int x = 0; x < _size; x++)
        {
            m_inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(ItemData _itemToAdd, int _amountToAdd)
    {
        m_inventorySlots[0] = new InventorySlot(_itemToAdd, _amountToAdd);
        return true;
    }
}
