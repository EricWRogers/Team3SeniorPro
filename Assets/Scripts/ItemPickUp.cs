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
    public void Interact(PlayerInventoryHolder _inventoryHolder)
    {
        if (_inventoryHolder.AddToInventory(itemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
