using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelMenu : MonoBehaviour
{
    #region Properties
    [Inject]
    private LevelManager _levelManager;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private Transform _levelContent;
    #endregion

    #region Methods
    #endregion

    #region Unity API
    void Start()
    {
        for (int i = 0; i < _levelManager.LevelList.Count; i++)
        {
            LevelButton _levelButton = Instantiate(_levelButtonPrefab, _levelContent);
            _levelButton.SetButton(_levelManager.LevelList[i], _levelManager);
        }
    }
    #endregion
}
