using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static MovableBloc;

public class PlayerGrab : MonoBehaviour
{
    [Inject]
    private PlayerBehavior playerBehavior;
    public IInteractable interactable;

    void Update()
    {
        if(interactable != null && interactable.GetType() == typeof(MovableBloc))
        {
            DragBehavior();
        }
    }

    private void DragBehavior()
    {
        if (interactable == null) return;
        MovableBloc movable = interactable as MovableBloc;
        Debug.Log($"[Ply] Drag {(interactable as MonoBehaviour).name}");
        if (movable == null) { return; }
        Debug.Log($"[Ply] Movable {(interactable as MonoBehaviour).name}");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"[Ply] Space {(interactable as MonoBehaviour).name}");
            playerBehavior.isDragging = true;
            movable.ActivateMove(true);  
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log($"[Ply] Not {(interactable as MonoBehaviour).name}");
            playerBehavior.isDragging = false;
            movable.ActivateMove(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Ply] TriggerEnter {other.name}");
        interactable = other.GetComponents<Component>().OfType<IInteractable>().FirstOrDefault();

    }

    private void OnTriggerExit(Collider other)
    {
        if (interactable == other.GetComponents<Component>().OfType<IInteractable>().FirstOrDefault())
        {
            MovableBloc movable = interactable as MovableBloc;
            movable.ActivateMove(false);
            interactable = null;
            playerBehavior.isDragging = false;
        }
    }
}
