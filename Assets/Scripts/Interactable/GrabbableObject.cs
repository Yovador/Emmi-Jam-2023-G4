using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float _distanceFromPlayer = 2;
    private Joint _joint;
    public Joint Joint
    {
        get
        {
            if(_joint == null)
            {
                _joint = GetComponent<Joint>();
            }
            return _joint;
        }
    }

    public void Interact(InteractionCallbackData callbackData)
    {
        callbackData.InteractableSource = gameObject;
        PlayerInteractor playerInteractor;
        if (callbackData.PlayerSource.TryGetComponent(out playerInteractor))
        {
            if(Joint.connectedBody == null)
            {
                Vector3 playerPos = callbackData.PlayerSource.transform.position;
                transform.position = new Vector3(playerPos.x, transform.position.y, playerPos.z) + callbackData.PlayerSource.transform.forward * _distanceFromPlayer;
                callbackData.Type = PlayerInteractType.Grab;
                Joint.connectedBody = playerInteractor.Rigidbody;
            }
            else
            {
                callbackData.Type = PlayerInteractType.Ungrab;
                Joint.connectedBody = null;
            }
        }
        CallbackAsync(callbackData).Forget();
    }

    private async UniTask CallbackAsync(InteractionCallbackData callbackData)
    {
        await UniTask.DelayFrame(1);
        callbackData.Callback.Invoke(callbackData);
    }
}
