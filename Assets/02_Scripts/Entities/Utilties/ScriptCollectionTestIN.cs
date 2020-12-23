using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCollectionTestIN : MonoBehaviour
{
    // public ScriptCollection collection;
    public int i = 3;
    
    private void OnEnable()
    {
        ScriptCollection.RegisterScript(this);
    }
    private void OnDisable()
    {
        ScriptCollection.RemoveScript(this);
    }
}
