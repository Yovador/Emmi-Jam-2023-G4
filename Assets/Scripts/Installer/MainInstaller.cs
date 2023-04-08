using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}