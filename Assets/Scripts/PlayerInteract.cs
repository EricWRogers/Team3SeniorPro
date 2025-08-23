using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{

    public UnityEvent ToggleCursor;
    public bool lookingAtItem = false;
    public bool lookingAtInteractable = false;
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
                lookingAtInteractable = true;
            }
            else
            {
                lookingAtInteractable = false;
            }

            if (hit.collider.CompareTag("Item"))
            {
                lookingAtItem = true;
            }
            else
            {
                lookingAtItem = false;
            }
        }
        else
        {
            lookingAtInteractable = false;
            lookingAtItem = false;
        }

        if (lookingAtItem && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with: " + hit.collider.name);
            hit.collider.GetComponent<ItemPickUp>().Interact(inventoryHolder);
            
        }
        if (lookingAtInteractable && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with: " + hit.collider.name);
            hit.collider.GetComponent<InteractionScript>().Interacted.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCursor.Invoke();
        }

    }

    public void ChangeCursor()
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
            playerMovement.lockedCursor = true;
        }
    }


    private void OnDrawGizmos()
    {
        if (lookingAtItem)Gizmos.color = Color.green;
        else if (lookingAtInteractable) Gizmos.color = Color.blue;
        else Gizmos.color = Color.red;
        Gizmos.DrawRay(startPos.position, startPos.forward * 2f);
    }
}
