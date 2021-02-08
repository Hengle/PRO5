using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private void OnEnable()
    {
        ScriptCollection.RegisterScript(this);
    }
    private void OnDisable()
    {
        ScriptCollection.RemoveScript(this);
        MyEventSystem.instance.resetPlayer -= ResetPlayer;
    }

    private void Start()
    {
        MyEventSystem.instance.resetPlayer += ResetPlayer;
    }

    private void ResetPlayer()
    {
        transform.position = new Vector3(0, -20, 0);
    }
}
