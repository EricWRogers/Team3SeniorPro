using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    //this script creates inventory 
    [SerializeField] private int m_inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;
    public int offset = 10;
    

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested;//inv to display, amount to offset

    protected virtual void Awake()
    {
        primaryInventorySystem = new InventorySystem(m_inventorySize);
    }

}
