using System;
using Unity.VisualScripting;
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
    public PlayerInventoryHolder inventoryHolder;
    private PlayerMovement playerMovement;
    public HotBarDisplay hotBarDisplay;
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            hotBarDisplay.UseItem();
        }

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

        if (Input.GetKeyDown(KeyCode.Tab) && !playerMovement.canMove)//if a interaction panel is open it will close it
        {
            ToggleCursor.Invoke();
        }

    }

    public void ChangeCursor()
    {
        if (playerMovement.canMove)
        {
            DisableMovemnent();
        }
        else
        {
            EnableMovement();
        }
    }

    public void EnableMovement()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.canMove = true;
        }
    }

    public void DisableMovemnent()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.canMove = false;
    }
    /*
    private void OnDrawGizmos()
    {
        if (lookingAtItem) Gizmos.color = Color.green;
        else if (lookingAtInteractable) Gizmos.color = Color.blue;
        else Gizmos.color = Color.red;
        Gizmos.DrawRay(startPos.position, startPos.forward * 2f);
    }
    */
}
