using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public UINavigation pauseMenu;
    public UINavigation controlMenu;
    public Button menuButton;

    public PlayerBehavior playerBehavior;

    private void Awake()
    {
        playerBehavior = FindObjectOfType<PlayerBehavior>(); //pas top, will need gpctrl or something to manage all reference later
    }

    public void PauseMenu()
    {
        if (canvasGroup.interactable)
        {
            pauseMenu.CloseMenu();
            canvasGroup.DOFade(0, .3f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            pauseMenu.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(true);
            playerBehavior.gamePaused = false;
        }
        else
        {
            canvasGroup.DOFade(1, .3f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            pauseMenu.gameObject.SetActive(true);
            pauseMenu.OpenMenu();
            menuButton.gameObject.SetActive(false);
            playerBehavior.gamePaused = true;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            PauseMenu();
        }   
    }
}
