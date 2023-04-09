using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Toggleable : MonoBehaviour
{
    [SerializeField]
    private bool _defaultState = false;

    [Header("Behaviour Events")]
    [SerializeField]
    private UnityEvent _onActivate;
    [SerializeField]
    private UnityEvent _onDeactivate;

    private bool _isOn = false;
    protected bool IsOn
    {
        get { return _isOn; }
        set
        {
            Debug.Log($"[Tgl] {name} IsOn from {_isOn} to {value}");
            if (value) { _onActivate.Invoke(); }
            else { _onDeactivate.Invoke(); }
            _isOn = value;
        }
    }

    [Inject]
    private void Injection()
    {
        _onActivate.AddListener(Activate);
        _onDeactivate.AddListener(Deactivate);
    }

    private void Start()
    {
        IsOn = _defaultState;
    }

    protected virtual void Activate()
    {
        Debug.Log("[Btn] Active");
    }

    protected virtual void Deactivate()
    {
        Debug.Log("[Btn] Deactive");
    }

    public void Switch()
    {
        if (IsOn)
        {
            SetOff();
        }
        else
        {
            SetOn();
        }
    }

    public void SetOn()
    {
        IsOn = true;
    }

    public void SetOff()
    {
        IsOn = false;
    }
}
