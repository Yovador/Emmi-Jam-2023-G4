using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

/// <summary>
/// Handles the Player Interaction with IInteractable objets
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerInteractor : MonoBehaviour
{
    [Inject]
    private PlayerStateMachine _playerStateMachine;
    [Inject(Id = "Player")]
    private Rigidbody _rb;
    public Rigidbody Rigidbody => _rb;



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
        foreach (var interractable in _currentInteractable)
        {
            InteractionCallbackData interactionCallback = new InteractionCallbackData(InteractionCallback, this);
            interractable.Interact(interactionCallback);
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

    private void InteractionCallback(InteractionCallbackData interactData)
    {
        switch (interactData.Type)
        {
            case PlayerInteractType.Grab:
                OnGrab(interactData);
                break;
            case PlayerInteractType.Ungrab:
                OnUngrab(interactData);
                break;
            default:
                break;
        }
    }

    private void OnGrab(InteractionCallbackData interactData)
    {
        _playerStateMachine.CurrentState.Value = PlayerState.Grabbing;
        AlignPlayer(interactData);
    }

    private void AlignPlayer(InteractionCallbackData interactData)
    {
        Vector3 dir = _playerStateMachine.transform.position + interactData.InteractableSource.transform.position;
        dir.Normalize();
        Vector3 fwd = interactData.InteractableSource.transform.forward;
        Vector3 rgt = interactData.InteractableSource.transform.right;
        float fwdDot = Vector3.Dot(dir, fwd);
        float rgtDot = Vector3.Dot(dir, rgt);
        Transform targetTransform = _playerStateMachine.transform;
        if (fwdDot > rgtDot)
        {
            if(fwdDot < 0)
            {
                targetTransform.forward = -new Vector3(fwd.x, 0, fwd.z) ;
            }
            else
            {
                targetTransform.forward = new Vector3(fwd.x, 0, fwd.z);
            }
        }
        else
        {
            if (rgtDot < 0)
            {
                targetTransform.forward = -new Vector3(rgt.x, 0, rgt.z);
            }
            else
            {
                targetTransform.forward = new Vector3(rgt.x, 0, rgt.z);
            }
        }
    }

    private void OnUngrab(InteractionCallbackData interactData)
    {
        _playerStateMachine.CurrentState.Value = PlayerState.Free;
    }
}
