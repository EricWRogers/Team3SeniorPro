using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public bool canInteract = false;
    public Transform startPos;
    public InventoryHolder inventoryHolder;
    private PlayerMovement playerMovement;
    void Start()
    {
        gameObject.TryGetComponent<PlayerMovement>(out playerMovement);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Ray(startPos.position, startPos.forward), out RaycastHit hit, 2f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                canInteract = true;
            }
            else
            {
                canInteract = false;
            }
        }
        else
        {
            canInteract = false;
        }

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with: " + hit.collider.name);
            hit.collider.GetComponent<ItemPickUp>().Interact(inventoryHolder);

        }

    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.lockedCursor = true;
    }
    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.lockedCursor = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = canInteract ? Color.green : Color.red;
        Gizmos.DrawRay(startPos.position, startPos.forward * 2f);
    }
}
