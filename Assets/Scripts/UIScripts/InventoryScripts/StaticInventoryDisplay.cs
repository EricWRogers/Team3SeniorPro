using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder m_inventoryHolder;
    [SerializeField] private InventorySlot_UI[] m_slots;


    protected override void Start()
    {
        base.Start();

        if (m_inventoryHolder != null)
        {
            inventorySystem = m_inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
        else Debug.LogWarning($"No Inventory assigned to {this.gameObject}");

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem _invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (m_slots.Length != inventorySystem.inventorySize)
        {
            Debug.Log($"Inventory slots out of sync on {this.gameObject}");
        }

        for (int x = 0; x < inventorySystem.inventorySize; x++)
        {
            slotDictionary.Add(m_slots[x], inventorySystem.InventorySlots[x]);
            m_slots[x].Init(inventorySystem.InventorySlots[x]);
        }
    }
}
