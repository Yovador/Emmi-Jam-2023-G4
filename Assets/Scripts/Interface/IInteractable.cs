using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implement this interface if your class needs to be interactable by the player.
/// </summary>
public interface IInteractable
{
    public void Interact(InteractionCallbackData interactData) {}
}
