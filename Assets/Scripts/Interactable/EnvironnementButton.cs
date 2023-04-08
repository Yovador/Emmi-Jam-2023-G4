using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironnementButton : Toggleable, IInteractable
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }
    }

    public void Interact()
    {
        Switch();
    }
}
