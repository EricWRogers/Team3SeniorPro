using System.Collections.Generic;
using UnityEngine;

public class WorkStations : MonoBehaviour
{
    public InventoryUIController UIController;
    private CraftingScript m_craftingScript;
    private PlayerInteract m_playerInteract;
    public bool isBroke = false;
    public List<ItemData> itemsNeedToRepair;
    void Awake()
    {
        m_playerInteract = GameObject.FindWithTag("Player").GetComponent<PlayerInteract>();
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
        else
        {
            if (m_craftingScript.TryRepair(itemsNeedToRepair))
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
