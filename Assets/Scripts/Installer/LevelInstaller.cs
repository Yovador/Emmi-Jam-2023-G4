using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    private LevelData _levelData;
    public override void InstallBindings()
    {
        Container.Bind<LevelData>().FromInstance(_levelData);
    }
}
