using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    Vector3 direction;
    public float speed;
    public Rigidbody rb;
    public bool isDragging = false;
    public PlayerGrab playerGrab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            if (!isDragging) { //normal player movement
                transform.forward = direction; 
                rb.velocity = direction * speed * Time.deltaTime;
            }
            else //dragging player movement
            {
                if (playerGrab.colliding != null && playerGrab.colliding.isActive && Input.GetKey(KeyCode.Space))
                {
                    switch (playerGrab.colliding.movementDirection)
                    {
                        case Interactable.MovementDirection.Horizontal:
                            playerGrab.colliding.rb.velocity = new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;
                            rb.velocity = new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;
                            break;
                        case Interactable.MovementDirection.Vertical:
                            playerGrab.colliding.rb.velocity = new Vector3(0, 0, direction.z) * speed * Time.deltaTime;
                            rb.velocity = new Vector3(0, 0, direction.z) * speed * Time.deltaTime;
                            break;
                    }
                }
            }

        }

    }
}
