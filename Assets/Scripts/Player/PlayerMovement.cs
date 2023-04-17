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
    }

    private void Update()
    {
        //Temporary, use OldInput System
        Movement((Quaternion.AngleAxis(_camRotation, Vector3.up) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized);
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

        Vector3 finalDirection = (_lastDirection + direction).normalized * _currentSpeed;
        Debug.Log($"[Mvt] {_currentMovementProfilType} Movement {finalDirection} / {_currentSpeed} / {_accelarationTime} / {_decelarationTime}");
        _rb.velocity = finalDirection;
        _lastDirection = finalDirection.normalized;


    }

    private void Accelerate()
    {

        if (_currentSpeed < _currentMovementProfil.Speed)
        {
            Debug.Log($"[Mvt] Accelerate");
            _accelarationTime += Time.fixedDeltaTime;
            _currentSpeed = GetCelerationFaction(_accelarationTime, _currentMovementProfil.AccelerationCurve);
        }
        else
        {
            _accelarationTime = 0;
        }
    }

    private void Decelerate()
    {
        if (_currentSpeed > 0)
        {
            Debug.Log($"[Mvt] Decelerate");
            _decelarationTime += Time.fixedDeltaTime;
            _currentSpeed = GetCelerationFaction(_decelarationTime, _currentMovementProfil.DecelerationCurve);
        }
        else
        {
            _decelarationTime = 0;
            _currentSpeed = 0;
        }
    }

    private float GetCelerationFaction(float time, AnimationCurve curve)
    {
        float celerationFactor = curve.Evaluate(time);
        return Mathf.Clamp((celerationFactor * _currentMovementProfil.Speed), 0, _currentMovementProfil.Speed);
    }

}
