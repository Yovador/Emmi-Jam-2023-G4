using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public CanvasGroup helpMenu;

    public void HelpMenu()
    {
        if (helpMenu.isActiveAndEnabled)
        {
            helpMenu.DOFade(0, .3f);
            helpMenu.interactable = false;
        } else
        {
            helpMenu.DOFade(1, .3f);
            helpMenu.interactable = true;
        }
    }
}
