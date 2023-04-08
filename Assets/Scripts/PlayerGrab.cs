using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using static Interactable;

public class PlayerGrab : MonoBehaviour
{

    public PlayerBehavior playerBehavior;
    public Interactable colliding;

    private void Start()
    {
        playerBehavior = GetComponentInParent<PlayerBehavior>();
    }

    void Update()
    {
        if (colliding == null) return;
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerBehavior.isDragging = true;
            colliding.ActivateMove(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        { 
            playerBehavior.isDragging = false;
            colliding.ActivateMove(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Interactable>() != null) colliding = other.GetComponentInParent<Interactable>();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if( colliding == other.GetComponentInParent<Interactable>())
        {
            colliding = null;
            playerBehavior.isDragging = false;
        }
    }
}
