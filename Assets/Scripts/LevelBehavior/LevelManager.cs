using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> _levelList = new List<LevelData>();

    private void Start()
    {
        if(_levelList.Count != 0)
        {
            LoadLevel(_levelList[0]);
        }
    }

    public void LoadLevel(LevelData level)
    {
        LoadLevelAsync(level).Forget();
    }

    private async UniTask LoadLevelAsync(LevelData level)
    {
        Debug.Log($"[Lvl] Start LoadLevel {level.SceneName}");
        await SceneManager.LoadSceneAsync(level.SceneName, LoadSceneMode.Additive);
        Debug.Log($"[Lvl] Finish LoadLevel {level.SceneName}");

    }

}
