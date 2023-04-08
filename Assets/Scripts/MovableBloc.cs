using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableBloc : MonoBehaviour, IInteractable
{
    public enum MovementDirection {
        Horizontal = 0,
        Vertical = 1
    }

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public bool isActive;
    [SerializeField]
    public MovementDirection movementDirection;
    [SerializeField]
    private Vector3 relativeToPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateMove(bool active)
    {
        Debug.Log($"[Mvb] ActivateMove {active}");
        if (active)
        {
            isActive = true;
            switch (movementDirection)
            {
                case MovementDirection.Horizontal:
                    Debug.Log($"[Mvb] Horizontal {active}");
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    break;
                case MovementDirection.Vertical:
                    Debug.Log($"[Mvb] Vertical {active}");
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
                    break;
            }
        }
        else
        {
            isActive = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        Debug.Log($"[Mvb] Constraints {rb.constraints}");
    }
}
