using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject terrain;
    private bool _isMoving;

    private Vector3 _defaultPos = Vector3.zero;
    private Quaternion _defaultRot = Quaternion.identity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (direction != Vector3.zero) _isMoving = true;
        else _isMoving = false;
        if (_playerGrab.interactable != null && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[Ply] Interact");
            _playerGrab.interactable.Interact();
        }
    }

    void FixedUpdate()
    {
        if (!isDragging) { //normal player movement
            if (_isMoving) transform.forward = direction;
            _rb.velocity = direction * _speed * Time.fixedDeltaTime;
        }
        else //dragging player movement
        {
            Debug.Log("[Ply] Player Drag");
            if (_playerGrab.interactable == null) { return; }
            GameObject interactObj = (_playerGrab.interactable as MonoBehaviour).gameObject;
            //if (interactObj.activeInHierarchy && Input.GetKey(KeyCode.Space))
            //{
                DraggingBehaviour();
            //}
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
                if ((direction.x > .5f && direction.z > .5f) || (direction.x < -.5f && direction.z < -.5f))
                {
                    movableBloc.rb.velocity = direction * _speed * Time.fixedDeltaTime;
                    _rb.velocity = direction * _speed * Time.fixedDeltaTime;
                }
                break;
            case MovableBloc.MovementDirection.Vertical:
                Debug.Log($"[Player] direction in vertical { direction} " );
                if ((direction.x > .5f && direction.z < -.5f) || (direction.x < -.5f && direction.z > .5f))
                {
                    movableBloc.rb.velocity = direction * _speed * Time.fixedDeltaTime;
                    _rb.velocity = direction * _speed * Time.fixedDeltaTime;
                }
                break;
            case MovableBloc.MovementDirection.All:
                    movableBloc.rb.velocity = direction * _speed * Time.fixedDeltaTime;
                    _rb.velocity = direction * _speed * Time.fixedDeltaTime;
                break;
        }
    }

    public void ReceiveLight()
    {
        Debug.Log($"Player is in the light");
        _rb.velocity = Vector3.zero;
        transform.position = _defaultPos;
        transform.rotation = _defaultRot;
        isDragging = false;
    }
}
