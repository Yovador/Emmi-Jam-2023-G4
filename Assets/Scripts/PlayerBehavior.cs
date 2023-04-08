using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour, ILightReceiver
{
    Vector3 direction;
    public float speed;
    public Rigidbody rb;
    public bool isDragging = false;
    public PlayerGrab playerGrab;
    public GameObject terrain;
    public bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (direction != Vector3.zero) isMoving = true;
        else isMoving = false;
     
        if (Input.GetKey(KeyCode.A)) //turn left
        {
            terrain.transform.RotateAround(transform.position, 5f);
        } 
        if (Input.GetKeyDown(KeyCode.E)) //turn right
        {

        }
    }

    private void FixedUpdate()
    {
        if (!isDragging) { //normal player movement
            if (isMoving) transform.forward = direction; 
            rb.velocity = direction * speed * Time.fixedDeltaTime;
        }
        else //dragging player movement
        {
            if (playerGrab.colliding != null && playerGrab.colliding.isActive && Input.GetKey(KeyCode.Space))
            {
                switch (playerGrab.colliding.movementDirection)
                {
                    case Interactable.MovementDirection.Horizontal:
                        if ((direction.x > 0.5f && direction.z > 0.5f) || (direction.x < -0.5f && direction.z < -0.5f)) 
                        {
                            playerGrab.colliding.rb.velocity = new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
                            rb.velocity = new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
                        }
                        break;
                    case Interactable.MovementDirection.Vertical:
                        if (direction.x > 0.5f && direction.z < -0.5f)
                        {
                            Debug.Log("right");
                            playerGrab.colliding.rb.velocity = new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
                            rb.velocity = new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
                        } 
                        if (direction.x < 0 && direction.z > 0)
                        {
                            Debug.Log("left");
                            playerGrab.colliding.rb.velocity = new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
                            rb.velocity = new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime;
                        }
                        break;
                }
            }
        }
    }

    public void ReceiveLight()
    {
        Debug.Log($"Player is in the light");
    }
}
