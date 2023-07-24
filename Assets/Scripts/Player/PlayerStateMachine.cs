using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// Contains and manage the state of the player
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
    /// <summary>
    /// The Current State of the Player Character
    /// </summary>
    public ReactiveProperty<PlayerState> CurrentState { get; private set; } = new ReactiveProperty<PlayerState>();
    /// <summary>
    /// The Current Movement State of the Player Character
    /// </summary>
    public ReactiveProperty<PlayerMovementState> CurrentMovementState { get; private set; } = new ReactiveProperty<PlayerMovementState>();

#region Editor Script
    #if UNITY_EDITOR
        //Code to Display player state in the inspector
        [Header("Debug")]
        [ReadOnly]
        [SerializeField]
        private PlayerState _playerState = PlayerState.Deactivated;
        [Inject]
        private void InjectEditor()
        {
            CurrentState.Subscribe(x => { _playerState = x; });
        }
    #endif
#endregion
}
