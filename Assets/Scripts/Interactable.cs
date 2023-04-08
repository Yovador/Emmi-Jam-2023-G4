using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum MovementDirection {
        Horizontal = 0,
        Vertical = 1
    }

    public Rigidbody rb;
    public Vector3 relativeToPlayer;
    public MovementDirection movementDirection;
    public bool isActive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void ActivateMove(bool active)
    {
        if (active)
        {
            isActive = true;
            switch (movementDirection)
            {
                case MovementDirection.Horizontal:
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                    break;
                case MovementDirection.Vertical:
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
                    break;
            }
        }
        else
        {
            isActive = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
