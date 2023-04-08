using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLightReceiver : MonoBehaviour, ILightReceiver
{
    public void ReceiveLight() 
    {
        Debug.Log($"[Lgt]{name} is in the light");
    }
}
