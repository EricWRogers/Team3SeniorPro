using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;

    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    public abstract void AssignSlot(InventorySystem _invToDisplay);

    protected virtual void Start()
    {

    }

    protected virtual void UpdateSlot(InventorySlot _updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == _updatedSlot)//slot value - the data of the slot
            {
                slot.Key.UpdateUISlot(_updatedSlot);//slot key - UI display of slot
            }
        }
    }



    public void SlotClicked(InventorySlot_UI _clickedUISlot)
    {
        bool isAltPressed = Keyboard.current.leftAltKey.isPressed;

        if (_clickedUISlot.AssignedInvSlot.ItemData != null && mouseInventoryItem.assignedInventorySlot.ItemData == null)
        {
            if (isAltPressed && _clickedUISlot.AssignedInvSlot.SplitStack(out InventorySlot halfStackSlot))//split stack
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                _clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(_clickedUISlot.AssignedInvSlot);//pick up item in inventory
                _clickedUISlot.ClearSlot();
                return;
            }
        }

        if (_clickedUISlot.AssignedInvSlot.ItemData == null && mouseInventoryItem.assignedInventorySlot.ItemData != null)//Place item in empty slot
        {
            _clickedUISlot.AssignedInvSlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
            _clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }

        if (_clickedUISlot.AssignedInvSlot.ItemData != null && mouseInventoryItem.assignedInventorySlot.ItemData != null)//both slots have an item
        {
            bool isSameItem = _clickedUISlot.AssignedInvSlot.ItemData == mouseInventoryItem.assignedInventorySlot.ItemData;

            //items are the same and has room so to combine
            if (isSameItem && _clickedUISlot.AssignedInvSlot.RoomLeftInStack(mouseInventoryItem.assignedInventorySlot.StackSize))
            {
                _clickedUISlot.AssignedInvSlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
                _clickedUISlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem && !_clickedUISlot.AssignedInvSlot.RoomLeftInStack(mouseInventoryItem.assignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1)//stack is full so swap 
                {
                    SwapSlots(_clickedUISlot);
                }
                else//slot has room so take what is need to fill stack and leave the rest
                {
                    int remainingOnMouse = mouseInventoryItem.assignedInventorySlot.StackSize - leftInStack;
                    _clickedUISlot.AssignedInvSlot.AddToStack(leftInStack);
                    _clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.assignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            else if (!isSameItem)// items are different so they swap
            {
                SwapSlots(_clickedUISlot);
                return;
            }
        }
    }

    private void SwapSlots(InventorySlot_UI _clickedSlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.assignedInventorySlot.ItemData, mouseInventoryItem.assignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(_clickedSlot.AssignedInvSlot);

        _clickedSlot.ClearSlot();
        _clickedSlot.AssignedInvSlot.AssignItem(clonedSlot);
        _clickedSlot.UpdateUISlot();
    }
}
