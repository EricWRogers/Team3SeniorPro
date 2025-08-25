using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    public static UnityAction OnPlayerInventoryChanged;

    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;
    public PlayerInteract player;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            player.DisableMovemnent();
            OnPlayerInventoryDisplayRequested?.Invoke(PrimaryInventorySystem, offset);
            
        }
    }

    public bool AddToInventory(ItemData _itemData, int _amount)
    {
        if (primaryInventorySystem.AddToInventory(_itemData, _amount))
        {
            return true;
        }

        return false;
    }
}
