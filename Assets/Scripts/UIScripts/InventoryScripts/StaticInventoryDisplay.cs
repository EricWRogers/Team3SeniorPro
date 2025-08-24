using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder m_inventoryHolder;
    [SerializeField] protected InventorySlot_UI[] m_slots;//Assigned in unity Inspector.

    protected virtual void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay;
    }

    protected virtual void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
    }

    private void RefreshStaticDisplay()
    {
        if (m_inventoryHolder != null)
        {
            inventorySystem = m_inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
        else Debug.LogWarning($"No Inventory assigned to {this.gameObject}");
        AssignSlot(inventorySystem, 0);
    }

    protected override void Start()
    {
        RefreshStaticDisplay();
    }

    public override void AssignSlot(InventorySystem _invToDisplay, int _offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        for (int x = 0; x < m_inventoryHolder.offset; x++)
        {
            slotDictionary.Add(m_slots[x], inventorySystem.InventorySlots[x]);
            m_slots[x].Init(inventorySystem.InventorySlots[x]);
        }
    }
}
