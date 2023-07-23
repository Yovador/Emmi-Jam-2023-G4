using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
//using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehavior : MonoBehaviour, ILightReceiver
{
    Vector3 direction;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _draggingSpeedDivider;
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
    public Action OnDestroy = new Action(() => { });
    [Inject(Id = "Transition")]
    private Animator _transitionAnimator;

    private Vector3 _defaultPos = Vector3.zero;
    private Quaternion _defaultRot = Quaternion.identity;

    private float _cameraRotation = -45;
    public Animator animator;
    public float CameraRotation => _cameraRotation;

    public bool gamePaused = false;
    public bool specialAnim = false;

    [Inject]
    private LevelManager _levelManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;
        DOTween.Init();
    }

    void Update()
    {
        if (gamePaused) {
            animator.SetBool("isWalking", false);
            animator.SetBool("isDragging", false);
            direction = Vector3.zero;
            return;
        }
        direction = Quaternion.AngleAxis(_cameraRotation, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (direction != Vector3.zero) _isMoving = true;
        else _isMoving = false;

        AnimatePlayer();
        if (_playerGrab.interactable != null && Input.GetButtonDown("Submit"))
        {
            Debug.Log("[Ply] Interact");
            _playerGrab.interactable.Interact((_)=>{});
            switch(_playerGrab.interactable)
            {
                case MovableBloc movableBlock:
                    Debug.Log("interaction with movable block");
                    break;
                case EnvironnementButton button:
                    Debug.Log("BUTTON");
                    switch(button.buttonType)
                    {
                        case EnvironnementButton.ButtonType.Button:
                            animator.SetTrigger("Button");
                            break;
                        case EnvironnementButton.ButtonType.Lever:
                            animator.SetTrigger("Lever");
                            break;
                    }
                    break;
            }
        }

        if (Input.GetButtonDown("CameraLeft")) //turn left
        {
            RotateTerrain(-1).Forget();
        }
        if (Input.GetButtonDown("CameraRight")) //turn right
        {
            RotateTerrain(1).Forget();
        }

    }

    private async UniTask RotateTerrain(int direction)
    {
        if (_isRotating){ return; }
        if (_virtualCam == null) return;
        _isRotating = true;
        _isMoving = false;
        _cameraRotation = (_cameraRotation - 90*direction) % 360;
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
                _rb.velocity = new Vector3(direction.x, 0, 0).normalized / _draggingSpeedDivider * _speed * Time.fixedDeltaTime;
                movableBloc.rb.velocity = new Vector3(direction.x, 0, 0).normalized/_draggingSpeedDivider  * _speed * Time.fixedDeltaTime;
                break;
            case MovableBloc.MovementDirection.Vertical:
                _rb.velocity = new Vector3(0, 0, direction.z).normalized/ _draggingSpeedDivider * _speed * Time.fixedDeltaTime;
                movableBloc.rb.velocity = new Vector3(0, 0, direction.z).normalized/ _draggingSpeedDivider * _speed * Time.fixedDeltaTime;
                break;
            case MovableBloc.MovementDirection.All:
                    movableBloc.rb.velocity = direction/ _draggingSpeedDivider * _speed * Time.fixedDeltaTime;
                    _rb.velocity = direction/ _draggingSpeedDivider * _speed * Time.fixedDeltaTime;
                break;
        }
    }

    public void ReceiveLight()
    {
        Debug.Log("SHOULD DIE HERE");
        gamePaused = true;
        _levelManager.ResetLevel();
        //Death().Forget();
    }

    private async UniTask Death()
    {
        Debug.Log($"Player is in the light");
        await UniTask.WaitWhile(() => _isRotating);
        _transitionAnimator.SetTrigger("Death");
        OnDestroy.Invoke();
        await UniTask.Delay(500);
        _transitionAnimator.ResetTrigger("Death");
        //if(_rb == null) { return; }
        //_rb.velocity = Vector3.zero;
        //transform.position = _defaultPos;
        //transform.rotation = _defaultRot;
        //isDragging = false;
        //animator.SetBool("isWalking", false);
        //animator.SetBool("isDragging", false);
        _levelManager.ResetLevel();

    }

    public void AnimatePlayer()
    {
        if (specialAnim) return;
        if (direction != Vector3.zero) //walking
        {
            animator.SetBool("isWalking", true);

        } else //idle
        {
            animator.SetBool("isWalking", false);
        }
        if (isDragging)
        {
            animator.SetBool("isDragging", true);
        }
        else
        {
            animator.SetBool("isDragging", false);
        }
    }

    public void JumpTo(Vector3 _position, Action onComplete = null)
    {
        specialAnim = true;
        transform.DOMove(_position, 1f).OnComplete(() =>
        {
            specialAnim = false;
            onComplete?.Invoke();
        });
    }


}
