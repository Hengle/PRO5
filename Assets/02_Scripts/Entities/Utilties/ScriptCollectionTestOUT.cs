using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCollectionTestOUT : MonoBehaviour
{
    // public ScriptCollection collection;
    public ScriptCollectionTestIN inTest;
    List<float> list = new List<float>();
    void Start()
    {
        inTest = ScriptCollection.GetScript<ScriptCollectionTestIN>();
    }

    private void OnDisable()
    {
        // float t = 0;
        // foreach (float l in list)
        //     t += l;

        // Debug.Log(t / list.Count + " Average ms");

    }
    void Update()
    {
        // float time = Time.realtimeSinceStartup;
        // for (int i = 0; i < 10000; i++)
        // {
        //     inTest = ScriptCollection.GetScript<ScriptCollectionTestIN>();
        // }
        // float end = (Time.realtimeSinceStartup - time) * 1000f;
        // list.Add(end);
        Debug.Log(inTest);
    }
}
