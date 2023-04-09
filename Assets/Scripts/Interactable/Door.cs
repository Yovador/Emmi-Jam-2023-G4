using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : Toggleable
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Activate()
    {
        base.Activate();
        _animator.SetTrigger("Open");
    }
    protected override void Deactivate()
    {
        base.Deactivate();
        _animator.SetTrigger("Close");
    }
}
