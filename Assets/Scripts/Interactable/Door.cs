using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : Toggleable
{
    [SerializeField]
    private AudioSource _open;
    [SerializeField]
    private AudioSource _close;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Activate()
    {
        base.Activate();
        _animator.SetTrigger("Open");
        _open.Play();
    }
    protected override void Deactivate()
    {
        base.Deactivate();
        _animator.SetTrigger("Close");
        _close.Play();
    }
}
