using UnityEngine;

public class WorkStations : MonoBehaviour
{
    public InventoryUIController UIController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InteractWithWorkStation()
    {
        if (!UIController.craftingUI.activeInHierarchy)
        {
            UIController.craftingUI.SetActive(true);
        }
        if (!UIController.inventoryBackground.gameObject.activeInHierarchy)
        {
            UIController.inventoryBackground.gameObject.SetActive(true);
        }
    }
}
