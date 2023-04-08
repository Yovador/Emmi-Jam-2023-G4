using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    private LevelData _levelData;
    [SerializeField]
    private GameObject _worldRoot;
    public override void InstallBindings()
    {
        Container.Bind<LevelData>().FromInstance(_levelData);
        Container.Bind<PlayerBehavior>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<List<ILightReceiver>>().FromInstance(LightReceivers());
        Container.Bind<GameObject>().WithId("World").FromInstance(_worldRoot);
    }

    //Get All the LightReceivers from the scene
    private List<ILightReceiver> LightReceivers()
    {
        List<ILightReceiver> result = new List<ILightReceiver>();

        List<GameObject> roots = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());

        IEnumerable<ILightReceiver> query = FindObjectsOfType<MonoBehaviour>().OfType<ILightReceiver>();
        foreach (ILightReceiver child in query)
        {
            result.Add(child);
        }
        Debug.Log($"[Lgt] result {result.Count}");
        return result;
    }
}
