using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject pauseMenu;
    public GameObject controlMenu;
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
            canvasGroup.DOFade(0, .3f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            pauseMenu.SetActive(false);
            menuButton.gameObject.SetActive(true);
            playerBehavior.gamePaused = false;
        }
        else
        {
            canvasGroup.DOFade(1, .3f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            pauseMenu.SetActive(true);
            menuButton.gameObject.SetActive(false);
            playerBehavior.gamePaused = true;
        }
    }

    public void ControlMenu()
    {
        if (controlMenu.gameObject.activeSelf)
        {
            controlMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
            pauseMenu.GetComponent<UINavigation>().selectedButton.Select();
        }
        else
        {
            controlMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
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
