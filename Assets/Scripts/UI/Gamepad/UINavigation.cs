using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINavigation : MonoBehaviour
{
    public List<ButtonAnimation> buttons;
    public Button selectedButton;

    private void Awake()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].uiNavigation = this;
        }
    }

    private void Start()
    {
        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "") return;
        selectedButton.Select();
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        if (Input.GetButtonDown("Submit"))
        {   
            selectedButton.onClick.Invoke();
        }
        return;
    }
}
