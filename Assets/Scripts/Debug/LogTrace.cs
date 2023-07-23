using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
