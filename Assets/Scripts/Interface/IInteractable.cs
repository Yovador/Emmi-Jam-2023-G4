using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void ShowCallToAction() { }
    public void HideCallToAction() { }
    public void Interact() { }
}
