using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;


/// <summary>
/// Handles the PlayerMovement
/// </summary>
[RequireComponent(typeof(PlayerStateMachine), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private List<PlayerMovementProfil> _movementProfils = new List<PlayerMovementProfil>() 
    { 
        new PlayerMovementProfil(PlayerMovementProfilType.Default), 
        new PlayerMovementProfil(PlayerMovementProfilType.Pushing),
        new PlayerMovementProfil(PlayerMovementProfilType.Freeze, 0),
    };

    private PlayerMovementProfilType _currentMovementProfilType;
    private PlayerMovementProfil _currentMovementProfil
    {
        get
        {
            var result = _movementProfils.Where(x => x.Type == _currentMovementProfilType).FirstOrDefault();
            if (result != null) 
            {
                return result;
            }
            result = _movementProfils.Where(x => x.Type == PlayerMovementProfilType.Default).FirstOrDefault();
            return result;
        }
    }
    private float _currentSpeed = 0;
    private float _accelarationTime = 0;
    private float _decelarationTime = 0;
    private Rigidbody _rb;
    private Vector3 _lastDirection = new Vector3();

    //BIG BIG TEMPORARY - NEED TO SEPERATE CAM CONTROL FROM OLD PLAYERBEHAVIOUR
    [Inject] private PlayerBehavior _playerBehavior;
    private float _camRotation => _playerBehavior.CameraRotation;

    [Inject] private PlayerStateMachine _playerStateMachine;
    [Inject]
    private void Bindings()
    {
        _playerStateMachine.CurrentState.Subscribe(x => PlayerStateCallback(x));
        _rb = GetComponent<Rigidbody>();
        _playerStateMachine.CurrentMovementState.Value = PlayerMovementState.Immobile;
    }

    private void PlayerStateCallback(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Free:
                _currentMovementProfilType = PlayerMovementProfilType.Default;
                break;
            case PlayerState.Grabbing:
                _currentMovementProfilType = PlayerMovementProfilType.Pushing;
                break;
            default:
                _currentMovementProfilType = PlayerMovementProfilType.Freeze;
                break;
        }
    }

    private void Start()
    {
        //DEBUG
        _playerStateMachine.CurrentState.Value = PlayerState.Free;
        _playerStateMachine.CurrentMovementState.Subscribe(x => Debug.Log($"[Ply] Currrent Movement State {x}"));
    }

    private void Update()
    {
        //Temporary, use OldInput System
        Movement( new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }

    private void Movement(Vector3 direction)
    {
        if(direction != Vector3.zero)
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }

        Vector3 finalDirection = CalculateDirection(direction) * _currentSpeed;
        _rb.velocity = finalDirection;
        _lastDirection = finalDirection.normalized;
    }

    private Vector3 CalculateDirection(Vector3 direction)
    {
        Vector3 result = (Quaternion.AngleAxis(_camRotation, Vector3.up) * direction).normalized;
        result = (_lastDirection + result).normalized;
        if (direction != Vector3.zero && _currentMovementProfil.RotationSpeed != 0)
        {
            if(transform.forward != result)
            {
                transform.forward += (result.normalized/10) * _currentMovementProfil.RotationSpeed;
                return transform.forward;
            }
        }
        return result;
    }

    private void Accelerate()
    {

        if (_currentSpeed < _currentMovementProfil.Speed)
        {
            _playerStateMachine.CurrentMovementState.Value = PlayerMovementState.Accelarating;
            _accelarationTime += Time.fixedDeltaTime;
            _currentSpeed = GetCelerationFactor(_accelarationTime, _currentMovementProfil.AccelerationCurve) * _currentMovementProfil.Speed;
        }
        else
        {
            _playerStateMachine.CurrentMovementState.Value = PlayerMovementState.Moving;
            _accelarationTime = 0;
        }
    }

    private void Decelerate()
    {
        if (_currentSpeed > 0)
        {
            _playerStateMachine.CurrentMovementState.Value = PlayerMovementState.Decelarating;
            _decelarationTime += Time.fixedDeltaTime;
            _currentSpeed = GetCelerationFactor(_decelarationTime, _currentMovementProfil.DecelerationCurve) * _currentSpeed;
        }
        else
        {
            _playerStateMachine.CurrentMovementState.Value = PlayerMovementState.Immobile;
            _decelarationTime = 0;
            _currentSpeed = 0;
        }
    }

    private float GetCelerationFactor(float time, AnimationCurve curve)
    {
        float celerationFactor = curve.Evaluate(time);
        return celerationFactor;
    }
}
