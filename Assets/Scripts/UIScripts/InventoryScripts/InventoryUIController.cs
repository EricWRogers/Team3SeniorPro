using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackPack;

    public GameObject InventoryBackground;

    void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackPack.gameObject.SetActive(false);
        InventoryBackground.SetActive(false);
    }
    void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryBackground.activeInHierarchy)
            {
                InventoryBackground.SetActive(false);
            }
            if (inventoryPanel.gameObject.activeInHierarchy)//disables the inventory 
            {
                InventoryBackground.SetActive(false);
            }
            if (playerBackPack.gameObject.activeInHierarchy)//disables the inventory 
            {
                playerBackPack.gameObject.SetActive(false);
            }
        }


    }

    private void DisplayInventory(InventorySystem _invToDisplay, int _offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        InventoryBackground.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(_invToDisplay, _offset);
    }
    private void DisplayPlayerInventory(InventorySystem _invToDisplay, int _offset)
    {
        if (_offset < _invToDisplay.inventorySize)
        {
            playerBackPack.gameObject.SetActive(true);
        }

        InventoryBackground.SetActive(true);
        playerBackPack.RefreshDynamicInventory(_invToDisplay, _offset);
    }


    public void ToggleChestInventory(int _invSize)
    {
        if (!inventoryPanel.gameObject.activeInHierarchy)
        {
            DisplayInventory(new InventorySystem(_invSize), 0);
        }
        else if (inventoryPanel.gameObject.activeInHierarchy)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
    }
}
