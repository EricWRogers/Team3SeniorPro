using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkStations : MonoBehaviour
{
    public InventoryUIController UIController;
    private RepairScript m_repairScript;
    private PlayerInteract m_playerInteract;
    public bool isBroke = false;
    public List<ItemData> itemsNeedToRepair = new();
    public TextMeshProUGUI missingItemsText;
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
            if (m_repairScript.TryRepair(itemsNeedToRepair, out List<ItemData> distinctItemList))
            {
                Debug.Log("repaired");
                isBroke = false;
            }
            else
            {
                missingItemsText.text = "";
                foreach (ItemData item in distinctItemList)
                {
                    missingItemsText.text = item.displayName.ToString() + " x" + m_repairScript.GetNumOfItem(itemsNeedToRepair, item).ToString();
                    missingItemsText.GetComponent<TextFade>().ResetFade();
                }
            }
        }

    }
}
