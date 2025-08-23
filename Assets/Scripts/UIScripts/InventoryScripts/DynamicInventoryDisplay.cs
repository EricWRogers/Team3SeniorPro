using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;

    protected override void Start()
    {
        base.Start();
    }


    public void RefreshDynamicInventory(InventorySystem _invToDisplay)
    {
        ClearSlots();
        inventorySystem = _invToDisplay;
        AssignSlot(_invToDisplay);
    }

    public override void AssignSlot(InventorySystem _invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (_invToDisplay == null)
        {
            return;
        }

        for (int x = 0; x < _invToDisplay.inventorySize; x++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, _invToDisplay.InventorySlots[x]);
            uiSlot.Init(_invToDisplay.InventorySlots[x]);
            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null)
        {
            slotDictionary.Clear();
        }
    }
}
