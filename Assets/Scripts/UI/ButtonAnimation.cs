using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [HideInInspector] public UINavigation uiNavigation;


    public void SelectButton()
    {
        if (Input.GetJoystickNames()[0] == "") return;
        transform.DOScale(1.1f, .3f);
        Debug.Log("selected : " + this.name);
    }

    public void DeselectButton()
    {
        transform.DOScale(1f, .3f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        uiNavigation.selectedButton = GetComponent<Button>();
        SelectButton();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DeselectButton();
    }
}
