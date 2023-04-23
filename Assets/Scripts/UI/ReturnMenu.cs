using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReturnMenu : MonoBehaviour
{
    [Inject]
    private LevelManager _levelManager;

    public void ReturnToMenu()
    {
        _levelManager.OpenMenu();
    }
}
