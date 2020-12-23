using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCollectionTestOUT : MonoBehaviour
{
    // public ScriptCollection collection;
    public ScriptCollectionTestIN inTest;
    void Start()
    {
        inTest = ScriptCollection.GetScript<ScriptCollectionTestIN>();
    }

    void Update()
    {

        Debug.Log(inTest.i);

    }
}
