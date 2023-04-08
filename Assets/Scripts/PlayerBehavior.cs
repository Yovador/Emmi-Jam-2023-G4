using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehavior : MonoBehaviour, ILightReceiver
{
    Vector3 direction;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private PlayerGrab _playerGrab;
    private Rigidbody _rb;
    [HideInInspector]
    public bool isDragging = false;
    public PlayerGrab playerGrab;
    public GameObject terrain;
    public bool isMoving;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (_playerGrab.interactable != null && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[Ply] Interact");
            _playerGrab.interactable.Interact();
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
    }

    void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            if (!isDragging) { //normal player movement
                transform.forward = direction;
                _rb.velocity = direction * _speed * Time.deltaTime;
            }
            else //dragging player movement
            {
                Debug.Log("[Ply] Player Drag");
                if (_playerGrab.interactable == null) { return; }
                GameObject interactObj = (_playerGrab.interactable as MonoBehaviour).gameObject;
                if (interactObj.activeInHierarchy && Input.GetKey(KeyCode.Space))
                {
                    DraggingBehaviour();
                }
            }
        }
    }

    void DraggingBehaviour()
    {
        Debug.Log("[Ply] Player Dragging Behaviour");
        if (_playerGrab.interactable.GetType() != typeof(MovableBloc)) { return; }
        MovableBloc movableBloc = _playerGrab.interactable as MovableBloc;
        Debug.Log($"[Ply] Movable Bloc = {movableBloc.name}");
        switch (movableBloc.movementDirection)
        {
            case MovableBloc.MovementDirection.Horizontal:
                movableBloc.rb.velocity = new Vector3(direction.x, 0, 0) * _speed * Time.deltaTime;
                _rb.velocity = new Vector3(direction.x, 0, 0) * _speed * Time.deltaTime;
                break;
            case MovableBloc.MovementDirection.Vertical:
                movableBloc.rb.velocity = new Vector3(0, 0, direction.z) * _speed * Time.deltaTime;
                _rb.velocity = new Vector3(0, 0, direction.z) * _speed * Time.deltaTime;
                break;
        }
    }

    public void ReceiveLight()
    {
        Debug.Log($"Player is in the light");
    }
}
