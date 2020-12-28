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
    }
}
