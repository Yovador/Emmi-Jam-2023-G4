using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class EndTrigger : MonoBehaviour
{
    [Inject]
    private LevelManager _levelManager;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehavior _player = other.GetComponent<PlayerBehavior>();
        if (_player != null)
        {
            Debug.Log("END");
            _player.JumpTo(transform.position, () =>
            {
                _levelManager.NextLevel();
            });
        }
    }
}
