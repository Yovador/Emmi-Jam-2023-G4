using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class AddNextLevelToChar : MonoBehaviour
{
    [Inject]
    private PlayerBehavior _playerBehavior;
    [Inject]
    private LevelManager _levelManager;
    private CancellationTokenSource _token = new CancellationTokenSource();

    void Start()
    {
        _playerBehavior.OnDestroy += () => OnPlayerDestroy().Forget();
    }

    private void OnDisable()
    {
        _token.Cancel();
    }

    private async UniTask OnPlayerDestroy()
    {
        await UniTask.Delay((int)(0.5 * 1000),false, PlayerLoopTiming.Update, _token.Token);
        _levelManager.NextLevel();
    }
}
