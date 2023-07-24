using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles the Player Interaction with IInteractable objets
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerInteractor : MonoBehaviour
{
    private List<IInteractable> _currentInteractable = new List<IInteractable>();

    private void Update()
    {
        //TODO : New Input System
        if (Input.GetButtonDown("Submit"))
        {
            Interact();
        }
    }

    private void Interact()
    {
        foreach (var collider in _currentInteractable)
        {
            collider.Interact(InteractionCallback);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponents<Component>().OfType<IInteractable>().FirstOrDefault();
        if(interactable == null) { return; }
        _currentInteractable.Add(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponents<Component>().OfType<IInteractable>().FirstOrDefault();
        if (interactable == null) { return; }
        _currentInteractable.Remove(interactable);
    }

    private void InteractionCallback(PlayerInteractType interactType)
    {
        switch (interactType)
        {
            case PlayerInteractType.Grab:
                OnGrab();
                break;
            default:
                break;
        }
    }

    private void OnGrab()
    {
    }
}
