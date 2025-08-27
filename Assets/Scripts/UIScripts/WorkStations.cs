using System.Collections.Generic;
using UnityEngine;

public class WorkStations : MonoBehaviour
{
    public InventoryUIController UIController;
    private RepairScript m_repairScript;
    private PlayerInteract m_playerInteract;
    public bool isBroke = false;
    public List<ItemData> itemsNeedToRepair = new();
    void Awake()
    {
        m_playerInteract = GameObject.FindWithTag("Player").GetComponent<PlayerInteract>();
        m_repairScript = gameObject.GetComponent<RepairScript>();
    }

    public void InteractWithWorkStation()
    {
        if (!isBroke)
        {
            if (!UIController.craftingUI.activeInHierarchy)
            {
                UIController.craftingUI.SetActive(true);
            }
            if (!UIController.inventoryBackground.gameObject.activeInHierarchy)
            {
                UIController.inventoryBackground.gameObject.SetActive(true);
            }
            m_playerInteract.ChangeCursor();
        }
        else if(isBroke)
        {
            Debug.Log("is broke");
            if (m_repairScript.TryRepair(itemsNeedToRepair))
            {
                Debug.Log("repaired");
                isBroke = false;
            }
            else
            {
                Debug.Log("missing items to repair");
            }
        }

    }
}
