using UnityEngine;
using UnityEditor;


public class Readme : MonoBehaviour
{
    public string show;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Readme))]
public class ReadMeEditor : Editor
{
    bool edit = false;
    public override void OnInspectorGUI()
    {
        Readme script = (Readme)target;
        GUILayout.Label("Instructions: ", EditorStyles.boldLabel);
        if (edit)
            script.show = EditorGUILayout.TextArea(script.show);
        else
            GUILayout.Label(script.show);

        GUILayout.Space(5f);

        if (!edit)
            if (GUILayout.Button("Edit"))
                edit = true;

        if (edit)
            if (GUILayout.Button("Save"))
            {
                edit = false;
                EditorUtility.SetDirty(script);
            }
    }
}
#endif