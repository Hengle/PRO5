using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GlobalEventSystem : MonoBehaviour
{
    public static GlobalEventSystem instance;
    public event System.Action onLoadFinish;
    public event System.Action onInit;
    public event System.Action onRestart;
    private void OnEnable()
    {
        instance = this;
    }

    public void OnLoadFinish()
    {
        onInit?.Invoke();
        onLoadFinish?.Invoke();
    }

    public void OnRestart()
    {
        onRestart?.Invoke();
    }
}
