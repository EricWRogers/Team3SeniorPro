using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public InventoryUIController inventoryUIController;
    private bool m_isInteracted = false;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public PlayerInteract player;

    public void Interact(PlayerInteract _interactor, out bool _interactSuccess)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, 0);
        _interactSuccess = true;
    }

    public bool EndInteraction()
    {
        inventoryUIController.ToggleChestInventory(primaryInventorySystem.inventorySize);
        return true;
    }

    public void InteractWithChest()
    {
            Debug.Log("Chest Open");
            Interact(player, out bool _chestOpened);
    }
}
