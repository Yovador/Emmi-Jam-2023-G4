using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamepadManager : MonoBehaviour
{
    #region Properties
    public bool isUsingGamepad;
    public UINavigation currentNavigation;
    public static GamepadManager Instance { get; private set; }
    #endregion

    #region Methods
    private bool AnyInput() //until new input system will detect any gamepad input
    {
        if ((Input.GetButtonDown("Cancel") ||
            Input.GetButtonDown("Submit") ||
            Input.GetButtonDown("CameraLeft") ||
            Input.GetButtonDown("CameraRight") ||
            Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0))
            return true;
        else return false;
    }
    #endregion

    #region Unity API
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null && currentNavigation != null && AnyInput()) 
            EventSystem.current.SetSelectedGameObject(currentNavigation.defaultSelectable.gameObject);

        if (Input.GetButtonDown("Cancel"))
        {
            if (currentNavigation != null) currentNavigation.OnCancel?.Invoke();
        }
    }
    #endregion
}
