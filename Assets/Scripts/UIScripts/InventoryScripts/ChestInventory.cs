using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder
{
    public InventoryUIController inventoryUIController;
    public void OpenChest()
    {
        Debug.Log("Chest Open");
        inventoryUIController.OpenDynamicInventory(inventorySystem.inventorySize);
    }
}
