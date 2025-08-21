using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int m_inventorySize;
    [SerializeField] protected InventorySystem m_inventorySystem;

    public InventorySystem inventorySystem => m_inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        m_inventorySystem = new InventorySystem(m_inventorySize);
    }

}
