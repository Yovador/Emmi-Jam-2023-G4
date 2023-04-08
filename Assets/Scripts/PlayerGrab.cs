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
            //colliding.transform.SetParent(playerBehavior.transform, true);
            playerBehavior.isDragging = true;
            colliding.ActivateMove(true);
            //colliding.relativeToPlayer = colliding.transform.position - playerBehavior.transform.position;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //switch(colliding.movementDirection)
            //{
            //    case MovementDirection.Horizontal:
            //        colliding.transform.position = new Vector3(playerBehavior.transform.position.x + colliding.relativeToPlayer.x, colliding.transform.position.y, colliding.transform.position.z);

            //        break;
            //    case MovementDirection.Vertical:
            //        colliding.transform.position = new Vector3(colliding.transform.position.x, colliding.transform.position.y, playerBehavior.transform.position.z + colliding.relativeToPlayer.z);

            //        //colliding.transform.position = playerBehavior.transform.position + colliding.relativeToPlayer;

            //        break;
            //}
            //colliding.transform.rotation = Quaternion.identity;
        } else
        {
            //colliding.transform.SetParent(null);
            playerBehavior.isDragging = false;
            colliding.ActivateMove(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            colliding = other.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("quit zone");
        if( colliding == other.GetComponent<Interactable>()) colliding = null;
    }
}
