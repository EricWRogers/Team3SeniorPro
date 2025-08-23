using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(PlayerInteract _interactor, out bool _interactSuccess);
    public bool EndInteraction();
}
