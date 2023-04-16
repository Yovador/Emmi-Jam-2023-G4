using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DummyPlayerTest : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine _playerStateMachine;
    private int i=0;

    private void Start()
    {
        _playerStateMachine.CurrentState.Subscribe(x => Debug.Log($"Dummy {x}"));
    }

    private void Update()
    {
        _playerStateMachine.CurrentState.Value = (PlayerState)i;
        i = (i + 1) % 6;
    }
}
