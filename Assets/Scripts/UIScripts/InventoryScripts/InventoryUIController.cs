using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

    void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }

    void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && inventoryPanel.gameObject.activeInHierarchy)//disables the inventory 
        {
            ToggleDynamicInventory(0);
        }


    }

    private void DisplayInventory(InventorySystem _invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(_invToDisplay);
    }

    public void ToggleDynamicInventory(int _invSize)
    {
        if (!inventoryPanel.gameObject.activeInHierarchy)
            {
                DisplayInventory(new InventorySystem(_invSize));
            }
            else if (inventoryPanel.gameObject.activeInHierarchy)
            {
                inventoryPanel.gameObject.SetActive(false);
            }
    }
}
