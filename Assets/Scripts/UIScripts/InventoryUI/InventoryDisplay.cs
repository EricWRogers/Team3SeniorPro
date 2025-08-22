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
        if (_clickedUISlot.assignedInvSlot.itemData != null && mouseInventoryItem.assignedInventorySlot.itemData == null)//pick up item in inventory
        {
            mouseInventoryItem.UpdateMouseSlot(_clickedUISlot.assignedInvSlot);
            _clickedUISlot.ClearSlot();
            return;
        }

        if (_clickedUISlot.assignedInvSlot.itemData == null && mouseInventoryItem.assignedInventorySlot.itemData != null)//Place item in empty slot
        {
            _clickedUISlot.assignedInvSlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
            _clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }

        if (_clickedUISlot.assignedInvSlot.itemData != null && mouseInventoryItem.assignedInventorySlot.itemData != null)//both slots have an item
        {
            bool isSameItem = _clickedUISlot.assignedInvSlot.itemData == mouseInventoryItem.assignedInventorySlot.itemData;

            //items are the same and has room so to combine
            if (isSameItem && _clickedUISlot.assignedInvSlot.RoomLeftInStack(mouseInventoryItem.assignedInventorySlot.stackSize))
            {
                _clickedUISlot.assignedInvSlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
                _clickedUISlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
            }
            else if (isSameItem && !_clickedUISlot.assignedInvSlot.RoomLeftInStack(mouseInventoryItem.assignedInventorySlot.stackSize, out int leftInStack))
            {
                if (leftInStack < 1)//stack is full so swap 
                {
                    SwapSlots(_clickedUISlot);
                }
                else//slot has room so take what is need to fill stack and leave the rest
                {
                    int remainingOnMouse = mouseInventoryItem.assignedInventorySlot.stackSize - leftInStack;
                    _clickedUISlot.assignedInvSlot.AddToStack(leftInStack);
                    _clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.assignedInventorySlot.itemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
            }
            else if (!isSameItem)// items are different so they swap
            {
                SwapSlots(_clickedUISlot);
            }
        }
    }

    private void SwapSlots(InventorySlot_UI _clickedSlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.assignedInventorySlot.itemData, mouseInventoryItem.assignedInventorySlot.stackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(_clickedSlot.assignedInvSlot);

        _clickedSlot.ClearSlot();
        _clickedSlot.assignedInvSlot.AssignItem(clonedSlot);
        _clickedSlot.UpdateUISlot();
    }
}
