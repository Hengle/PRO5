using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GlobalEventSystem : MonoBehaviour
{
    public static GlobalEventSystem instance;
    public event System.Action onLoadFinish;
    private void OnEnable()
    {
        instance = this;
    }

    public void OnLoadFinish()
    {
        onLoadFinish?.Invoke();
    }
}
