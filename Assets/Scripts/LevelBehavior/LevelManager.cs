using Cysharp.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> _levelList = new List<LevelData>();
    [Inject(Id = "Transition")]
    private Animator _transitionAnimator;
    private int _currentLvl = 0;

    public void StartLevel()
    {
        if (_levelList.Count != 0)
        {
            LoadLevel(_levelList[0]);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            ResetLevel();
        }
    }

    public void LoadLevel(LevelData level)
    {
        LoadLevelAsync(level).Forget();

    }

    public void NextLevel()
    {
        UnloadLevelAsync(_levelList[_currentLvl]).Forget();
        LoadLevel(_levelList[_currentLvl+1]);
    }

    public void ResetLevel()
    {
        //LoadLevel(_levelList[_currentLvl]);
        ReloadLevelAsync(_levelList[_currentLvl]).Forget();
    }


    private async UniTask UnloadLevelAsync(LevelData level)
    {
        if (SceneManager.GetAllScenes().Where(x => x.name == level.SceneName).Count() != 0)
        {
            await SceneManager.UnloadSceneAsync(level.SceneName);
        }
    }

    private async UniTask LoadLevelAsync(LevelData level)
    {
        _transitionAnimator.SetTrigger("Next");
        await UniTask.Delay(700);
        await UnloadLevelAsync(level);
        Debug.Log($"[Lvl] Start LoadLevel {level.SceneName}");
        await SceneManager.LoadSceneAsync(level.SceneName, LoadSceneMode.Additive);
        Debug.Log($"[Lvl] Finish LoadLevel {level.SceneName}");
        _currentLvl = _levelList.IndexOf(level);
    }

    private async UniTask ReloadLevelAsync(LevelData level)
    {
        _transitionAnimator.SetTrigger("Death");
        await UniTask.Delay(300);
        _transitionAnimator.ResetTrigger("Death");
        await UnloadLevelAsync(level);
        Debug.Log($"[Lvl] Start LoadLevel {level.SceneName}");
        await SceneManager.LoadSceneAsync(level.SceneName, LoadSceneMode.Additive);
        Debug.Log($"[Lvl] Finish LoadLevel {level.SceneName}");
        _currentLvl = _levelList.IndexOf(level);
    }

    public void OpenMenu()
    {
        OpenMenuAsync();
    }

    private async UniTask OpenMenuAsync()
    {
        await UnloadLevelAsync(_levelList[_currentLvl]);
        _currentLvl = 0;
        _transitionAnimator.SetTrigger("Next");
        await UniTask.Delay(750);
        await SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }

}
