using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCallbackData
{
    public Action<InteractionCallbackData> Callback { get; set; } = (x) => {};

    public PlayerInteractType Type { get; set; } = PlayerInteractType.Default;

    public GameObject InteractableSource { get; set; } = null;

    public PlayerInteractor PlayerSource { get; set; } = null;

    public InteractionCallbackData()
    {
    }

    public InteractionCallbackData(Action<InteractionCallbackData> callback, PlayerInteractor playerInteractor)
    {
        Callback = callback;
        PlayerSource = playerInteractor;
    }
}
