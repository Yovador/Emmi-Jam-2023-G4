using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironnementButton : Toggleable, IInteractable
{
    public void Interact()
    {
        Switch();
    }
}
