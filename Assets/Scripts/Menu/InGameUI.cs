using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public CanvasGroup helpMenu;
    public Button helpButton;

    public void HelpMenu()
    {
        if (helpMenu.interactable)
        {
            Time.timeScale = 1;
            helpMenu.DOFade(0, .3f);
            helpMenu.interactable = false;
            helpButton.gameObject.SetActive(true);
            helpMenu.blocksRaycasts = false;
        } else
        {
            
            helpMenu.DOFade(1, .3f).OnComplete(() => {
                Time.timeScale = 0;
            }) ;
            helpMenu.interactable = true;
            helpMenu.blocksRaycasts = true;
        }
    }
}
