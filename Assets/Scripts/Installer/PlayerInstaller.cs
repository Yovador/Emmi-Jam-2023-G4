using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _player;
    public override void InstallBindings()
    {
        Container.Bind<PlayerStateMachine>().FromComponentOn(_player).AsSingle().NonLazy();
        Container.Bind<Rigidbody>().WithId("Player").FromComponentOn(_player).AsSingle().NonLazy();
    }
}
