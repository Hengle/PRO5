using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GlobalEventSystem : MonoBehaviour
{
    public static GlobalEventSystem instance;
    public event System.Action onLoadFinish;
    public event System.Action onInit;
    private void OnEnable()
    {
        instance = this;
    }

    public void OnLoadFinish()
    {
        onInit?.Invoke();
        onLoadFinish?.Invoke();
    }
}
