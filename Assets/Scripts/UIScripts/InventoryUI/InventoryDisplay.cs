using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem m_inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> m_slotDictionary;
    public InventorySystem inventorySystem => m_inventorySystem;

    public Dictionary<InventorySlot_UI, InventorySlot> slotDictionary => m_slotDictionary;

    public abstract void AssignSlot(InventorySystem _invToDisplay);

    protected virtual void Start()
    {
        
    }

    protected virtual void UpdateSlot(InventorySlot _updatedSlot)
    {
        foreach (var slot in m_slotDictionary)
        {
            if (slot.Value == _updatedSlot)//slot value - the data of the slot
            {
                slot.Key.UpdateUISlot(_updatedSlot);//slot key - UI display of slot
            }
        }
    }

    public void SlotClicked(InventorySlot_UI _clickedUISlot)
    {
        if (_clickedUISlot.assignedInvSlot.itemData != null && mouseInventoryItem.assignedInventorySlot.itemData == null)
        {
            mouseInventoryItem.UpdateMouseSlot(_clickedUISlot.assignedInvSlot);
            _clickedUISlot.ClearSlot();
            return;
        }
    }
}
