using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Log message with logId as prefix only if trace is true
/// </summary>
[System.Serializable]
public class LogTrace
{
    [Header("Debug")]
    [SerializeField]
    private string _logId = "Dft";
    [SerializeField]
    private bool _trace= true;

    public void Log(string message)
    {
        if (_trace)
        {
            Debug.Log($"[{_logId}] {message}");
        }
    }
}
