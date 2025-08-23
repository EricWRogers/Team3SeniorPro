using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;
    public Transform player;

    void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
    public void UpdateMouseSlot(InventorySlot _invSlot)
    {
        assignedInventorySlot.AssignItem(_invSlot);
        itemSprite.sprite = _invSlot.ItemData.image;
        itemCount.text = _invSlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    void Update()
    {
        if (assignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                DropOneItemFromStack(assignedInventorySlot);
                
            }

        }
    }

    public void DropOneItemFromStack(InventorySlot _slotToDrop)
    {
        if (_slotToDrop != null)
        {
            if (_slotToDrop.StackSize > 1)
            {
                Instantiate(_slotToDrop.ItemData.itemPrefab, new Vector3(player.position.x, player.position.y - 1, player.position.z), Quaternion.identity);
                _slotToDrop.RemoveFromStack(1);
            }
            else
            {
                Instantiate(_slotToDrop.ItemData.itemPrefab, new Vector3(player.position.x, player.position.y - 1, player.position.z), Quaternion.identity);
                ClearSlot();
            }
            
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
