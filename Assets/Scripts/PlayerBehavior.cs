using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
//using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehavior : MonoBehaviour, ILightReceiver
{
    Vector3 direction;
    [SerializeField]
    private float _speed;
    [Inject]
    private CinemachineVirtualCamera _virtualCam;
    [SerializeField]
    private float _cameraRotateSpeed = 0.5f;
    [SerializeField]
    private PlayerGrab _playerGrab;
    private Rigidbody _rb;
    [HideInInspector]
    public bool isDragging = false;
    private bool _isMoving;
    private bool _isRotating = false;

    private Vector3 _defaultPos = Vector3.zero;
    private Quaternion _defaultRot = Quaternion.identity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;
        DOTween.Init();
    }

    void Update()
    {

        direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (direction != Vector3.zero) _isMoving = true;
        else _isMoving = false;
        if (_playerGrab.interactable != null && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("[Ply] Interact");
            _playerGrab.interactable.Interact();
        }

        if (Input.GetKeyDown(KeyCode.A)) //turn left
        {
            RotateTerrain(-1).Forget();
        }
        if (Input.GetKeyDown(KeyCode.E)) //turn right
        {
            RotateTerrain(1).Forget();
        }

    }

    private async UniTask RotateTerrain(int direction)
    {
        if (_isRotating){ return; }
        _isRotating = true;
        _isMoving = false;
        var dolly = _virtualCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        DOTween.To(() => dolly.m_PathPosition, x => dolly.m_PathPosition = x, dolly.m_PathPosition+direction, _cameraRotateSpeed);
        await UniTask.Delay((int)(_cameraRotateSpeed * 1000));
        _isRotating = false;
    }

    void FixedUpdate()
    {
        if (_isRotating) { return; }
        if (!isDragging) { //normal player movement
            if (_isMoving) transform.forward = direction;
            _rb.velocity = direction * _speed * Time.fixedDeltaTime;
        }
        else //dragging player movement
        {
            Debug.Log("[Ply] Player Drag");
            if (_playerGrab.interactable == null) { return; }
            GameObject interactObj = (_playerGrab.interactable as MonoBehaviour).gameObject;
            DraggingBehaviour();
            
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
                    movableBloc.rb.velocity = direction  * _speed * Time.fixedDeltaTime;
                    _rb.velocity = direction * _speed * Time.fixedDeltaTime;
                }
                break;
            case MovableBloc.MovementDirection.Vertical:
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
        Death().Forget();
    }

    private async UniTask Death()
    {
        Debug.Log($"Player is in the light");
        await UniTask.WaitWhile(() => _isRotating);
        _rb.velocity = Vector3.zero;
        transform.position = _defaultPos;
        transform.rotation = _defaultRot;
        isDragging = false;
    }
}
