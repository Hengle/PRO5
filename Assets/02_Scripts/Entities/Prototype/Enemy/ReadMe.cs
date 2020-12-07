using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
public class ReadMe : MonoBehaviour
{
    [SerializeField] public string show;
}

[CustomEditor(typeof(ReadMe))]
public class InstructionEditor : Editor
{
    bool edit = false;
    public override void OnInspectorGUI()
    {

        ReadMe script = (ReadMe)target;
        GUILayout.Label("Enter any instruction for this object here: ", EditorStyles.boldLabel);
        if (edit)
            script.show = EditorGUILayout.TextArea(script.show);
        else
            GUILayout.Label(script.show);

        if (!edit)
            if (GUILayout.Button("Edit"))
                edit = true;

        if (edit)
            if (GUILayout.Button("Save"))
            {
                edit = false;
            }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }
}
#endif