using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName ="_LevelData", menuName ="JAM/Level/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string _sceneName;
    public string SceneName => _sceneName;

    [SerializeField]
    private string _levelDisplayName;
    public string LevelDisplayName => _levelDisplayName;

}
