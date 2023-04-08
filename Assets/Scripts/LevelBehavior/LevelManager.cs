using Cysharp.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> _levelList = new List<LevelData>();

    private void Start()
    {
        //Temporary
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
        if (SceneManager.GetAllScenes().Where(x => x.name == level.SceneName).Count() != 0)
        {
            await SceneManager.UnloadSceneAsync(level.SceneName);
        }
        Debug.Log($"[Lvl] Start LoadLevel {level.SceneName}");
        await SceneManager.LoadSceneAsync(level.SceneName, LoadSceneMode.Additive);
        Debug.Log($"[Lvl] Finish LoadLevel {level.SceneName}");

    }

}
