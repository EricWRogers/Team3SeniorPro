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
            m_inventorySystem = m_inventoryHolder.inventorySystem;
            m_inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
        else Debug.LogWarning($"No Inventory assigned to {this.gameObject}");

        AssignSlot(m_inventorySystem);
    }

    public override void AssignSlot(InventorySystem _invToDisplay)
    {
        m_slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (m_slots.Length != m_inventorySystem.inventorySize)
        {
            Debug.Log($"Inventory slots out of sync on {this.gameObject}");
        }

        for (int x = 0; x < m_inventorySystem.inventorySize; x++)
        {
            m_slotDictionary.Add(m_slots[x], m_inventorySystem.inventorySlots[x]);
            m_slots[x].Init(m_inventorySystem.inventorySlots[x]);
        }
    }
}
