using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UINavigation : MonoBehaviour
{
    public Selectable defaultSelectable;
    public UnityEvent OnCancel;

    private void Start()
    {
        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "") return;
        //EventSystem.current.SetSelectedGameObject(defaultSelectable.gameObject);
    }

    public void OpenMenu()
    {
        if(EventSystem.current.currentSelectedGameObject!= null)EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().OnDeselect(null);
        GamepadManager.Instance.currentNavigation = this;
        EventSystem.current.SetSelectedGameObject(defaultSelectable.gameObject);
    }

    public void CloseMenu()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().OnDeselect(null);
        GamepadManager.Instance.currentNavigation = null;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
