using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.Mathematics;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;
    private Transform m_player;
    public float ItemDropOffset = 1;

    void Awake()
    {
        itemSprite.color = Color.clear;
        itemSprite.preserveAspect = true;
        itemCount.text = "";

        m_player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (m_player = null) Debug.LogWarning("Player not found");
    }
    public void UpdateMouseSlot(InventorySlot _invSlot)
    {
        assignedInventorySlot.AssignItem(_invSlot);
        UpdateMouseSlot();
    }
    public void UpdateMouseSlot()
    {
        itemSprite.sprite = assignedInventorySlot.ItemData.image;
        itemCount.text = assignedInventorySlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    void Update()
    {
        if (assignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                Instantiate(assignedInventorySlot.ItemData.itemPrefab, m_player.position + m_player.forward * ItemDropOffset, quaternion.identity);

                if (assignedInventorySlot.StackSize > 1)
                {
                    assignedInventorySlot.AddToStack(-1);
                    UpdateMouseSlot();
                }
                else
                    ClearSlot();
            }

        }
    }

    public void DropOneItemFromStack(InventorySlot _slotToDrop)
    {
        if (_slotToDrop != null)
        {
            if (_slotToDrop.StackSize > 1)
            {
                Instantiate(_slotToDrop.ItemData.itemPrefab, new Vector3(m_player.position.x, m_player.position.y - 1, m_player.position.z), Quaternion.identity);
                _slotToDrop.RemoveFromStack(1);
            }
            else
            {
                Instantiate(_slotToDrop.ItemData.itemPrefab, new Vector3(m_player.position.x, m_player.position.y - 1, m_player.position.z), Quaternion.identity);
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
