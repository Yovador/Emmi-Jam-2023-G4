using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartGame : MonoBehaviour
{
    [Inject]
    private LevelManager _levelManager;

    public void StartTheGame()
    {
        _levelManager.StartLevel();
        SceneManager.UnloadSceneAsync("Menu");
    }
}
