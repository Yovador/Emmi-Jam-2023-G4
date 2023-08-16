using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [HideInInspector] public UINavigation uiNavigation;

    public void OnSelect(BaseEventData eventData)
    {
        OnPointerEnter(null);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, .3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("start exit");
        transform.DOScale(1f, .3f).OnComplete(() =>
        {
            Debug.Log("end exit");
            transform.localScale = Vector3.one;
        });
    }
}
