using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackPack;

    public GameObject inventoryBackground;

    public GameObject craftingUI;

    void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackPack.gameObject.SetActive(false);
        inventoryBackground.SetActive(false);
        craftingUI.SetActive(false);
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
            CloseAllDisplays();
        }

    }

    private void DisplayInventory(InventorySystem _invToDisplay, int _offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryBackground.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(_invToDisplay, _offset);
    }
    private void DisplayPlayerInventory(InventorySystem _invToDisplay, int _offset)
    {
        if (_offset < _invToDisplay.inventorySize)
        {
            playerBackPack.gameObject.SetActive(true);
        }

        inventoryBackground.SetActive(true);
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

    public void CloseAllDisplays()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (inventoryBackground.gameObject.activeInHierarchy)
        {
            inventoryBackground.gameObject.SetActive(false);
        }
    }
}
