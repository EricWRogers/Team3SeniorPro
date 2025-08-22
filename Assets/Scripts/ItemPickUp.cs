using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData itemData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact(InventoryHolder _inventoryHolder)
    {
        if (_inventoryHolder.inventorySystem.AddToInventory(itemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
