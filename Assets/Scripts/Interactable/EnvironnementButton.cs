using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironnementButton : Toggleable, IInteractable
{
    private AudioSource _audioSource;
    public enum ButtonType
    {
        Button = 0,
        Lever = 1
    }

    private Animator _animator;
    public ButtonType buttonType;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.mute = true;
        AsyncActivateSound().Forget();
    }

    private async UniTask AsyncActivateSound()
    {
        await UniTask.Delay(1000);
        await UniTask.WaitUntil(() => _audioSource != null);
        _audioSource.mute = false;
    }

    public void Interact(InteractionCallbackData callbackData)
    {
        _audioSource?.Play();
        Switch();
    }

    protected override void Activate()
    {
        if(_animator == null) { return; }
        base.Activate();
        _animator.SetTrigger("Open");
    }

    protected override void Deactivate()
    {
        if (_animator == null) { return; }
        base.Deactivate();
        _animator.SetTrigger("Close");
    }
}
