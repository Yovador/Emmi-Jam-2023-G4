using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadScene : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;
    void Start()
    {
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
    }

}
