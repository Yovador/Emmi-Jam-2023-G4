using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironnementButton : Toggleable, IInteractable
{
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
    }
    public void Interact()
    {
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
