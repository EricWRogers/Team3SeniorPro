using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{

    public UnityEvent ToggleCursor;
    public bool canInteract = false;
    public Transform startPos;
    public InventoryHolder inventoryHolder;
    private PlayerMovement playerMovement;
    void Start()
    {
        gameObject.TryGetComponent<PlayerMovement>(out playerMovement);
        if(ToggleCursor == null)
        {
            ToggleCursor = new UnityEvent();
        }
        ToggleCursor.AddListener(ChangeCursor);
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCursor.Invoke();
        }

    }

    private void ChangeCursor()
    {
        Cursor.visible = !Cursor.visible;
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            playerMovement.lockedCursor = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.lockedCursor = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = canInteract ? Color.green : Color.red;
        Gizmos.DrawRay(startPos.position, startPos.forward * 2f);
    }
}
