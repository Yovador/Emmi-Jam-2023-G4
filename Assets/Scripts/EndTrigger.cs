using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EndTrigger : MonoBehaviour
{
    [Inject]
    private LevelManager _levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerBehavior>() != null)
        {
            Debug.Log("END");
            _levelManager.NextLevel();
        }
    }
}
