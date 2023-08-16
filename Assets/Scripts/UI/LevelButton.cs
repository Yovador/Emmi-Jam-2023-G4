using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    #region Properties
    private LevelManager _levelManager;
    private LevelData _levelData;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    #endregion

    #region Methods
    public void SetButton(LevelData _data, LevelManager _manager)
    {
        _levelData = _data;
        _text.text = _data.LevelDisplayName;
        _levelManager = _manager;
        //check save
        //if (unlocked) _button.interactable = true;
        //else _button.interactable = false;
    }

    public void LaunchLevel()
    {
        _levelManager.LoadLevel(_levelData);
        SceneManager.UnloadSceneAsync("Menu");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = Color.white;
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnPointerEnter(null);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnPointerExit(null);
    }
    #endregion
}
