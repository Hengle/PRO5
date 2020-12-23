using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCollectionTestIN : MonoBehaviour
{
    // public ScriptCollection collection;
    public int i = 3;
    private void Awake()
    {
        ScriptCollection.RegisterScript(this);
    }
}
