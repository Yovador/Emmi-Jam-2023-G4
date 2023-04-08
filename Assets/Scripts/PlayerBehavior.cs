using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour, ILightReceiver
{

    Vector3 direction;
    public float speed;
    public Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            gameObject.transform.forward = direction;
            rb.velocity = direction * speed * Time.deltaTime;

        }

    }

    public void ReceiveLight()
    {
        Debug.Log($"Player is in the light");
    }
}
