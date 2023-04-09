using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Animator _transitionAnimator;
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Animator>().WithId("Transition").FromInstance(_transitionAnimator).AsSingle().NonLazy();
    }
}