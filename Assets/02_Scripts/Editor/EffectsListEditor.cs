using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EffectsListEditor : EditorWindow
{
    SoundEffectController[] sArr;
    ParticleEffectController[] pArr;

    string SFXsearchText = "";
    string PFXsearchText = "";

    bool SFXfoldout = true;
    bool PFXfoldout = true;

    [MenuItem("Tools/Custom Window/Effects list")]
    public static void ShowWindow()
    {
        GetWindow<EffectsListEditor>("Effects list");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("RefreshList"))
        {
            sArr = GameObject.FindObjectsOfType<SoundEffectController>();
            pArr = GameObject.FindObjectsOfType<ParticleEffectController>();
        }
        GUILayout.Space(15f);

        SFXfoldout = EditorGUILayout.Foldout(SFXfoldout, "SFX List");
        if (SFXfoldout)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Search SFX", EditorStyles.boldLabel);
            SFXsearchText = EditorGUILayout.TextField(SFXsearchText);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("SFX List", EditorStyles.boldLabel);
            GUILayout.Space(10f);

            if (sArr != null && sArr.Length != 0)
                for (int i = 0; i < sArr.Length; i++)
                {
                    if (sArr[i] != null)
                        if (sArr[i].effectName.ToLower().Contains(SFXsearchText.ToLower()))
                        {
                            sArr[i] = (SoundEffectController)EditorGUILayout.ObjectField(sArr[i], typeof(SoundEffectController), false, GUILayout.MaxWidth(150f));

                            sArr[i].soundEffect = (FMODUnity.StudioEventEmitter)EditorGUILayout.ObjectField(sArr[i].soundEffect, typeof(FMODUnity.StudioEventEmitter), true, GUILayout.MaxWidth(150f));
                            GUILayout.BeginHorizontal();

                            sArr[i].effectName = EditorGUILayout.TextField(sArr[i].effectName, GUILayout.MaxWidth(70f));

                            GUILayout.Label("On Collision", GUILayout.MaxWidth(70f));
                            sArr[i].playOnCollision = EditorGUILayout.Toggle(sArr[i].playOnCollision, GUILayout.MaxWidth(20f));

                            GUILayout.Label("On Command", GUILayout.MaxWidth(80f));
                            sArr[i].playOnCommand = EditorGUILayout.Toggle(sArr[i].playOnCommand, GUILayout.MaxWidth(20f));

                            GUILayout.Label("Active", GUILayout.MaxWidth(45f));
                            sArr[i].active = EditorGUILayout.Toggle(sArr[i].active, GUILayout.MaxWidth(20f));
                            GUILayout.EndHorizontal();
                            GUILayout.Space(15f);
                        }
                }
        }

        GUILayout.Space(20f);

        PFXfoldout = EditorGUILayout.Foldout(PFXfoldout, "PFX List");
        if (PFXfoldout)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Search PFX", EditorStyles.boldLabel);
            PFXsearchText = EditorGUILayout.TextField(PFXsearchText);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label("PFX List", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            if (pArr != null && pArr.Length != 0)
                for (int i = 0; i < pArr.Length; i++)
                {
                    if (pArr[i].effectName.ToLower().Contains(PFXsearchText.ToLower()))
                    {
                        pArr[i] = (ParticleEffectController)EditorGUILayout.ObjectField(pArr[i], typeof(ParticleEffectController), false, GUILayout.MaxWidth(150f));

                        pArr[i].particle = (ParticleSystem)EditorGUILayout.ObjectField(pArr[i].particle, typeof(ParticleSystem), true, GUILayout.MaxWidth(150f));
                        GUILayout.BeginHorizontal();

                        pArr[i].effectName = EditorGUILayout.TextField(pArr[i].effectName, GUILayout.MaxWidth(70f));

                        GUILayout.Label("On Collision", GUILayout.MaxWidth(70f));
                        pArr[i].playOnCollision = EditorGUILayout.Toggle(pArr[i].playOnCollision, GUILayout.MaxWidth(20f));

                        GUILayout.Label("On Command", GUILayout.MaxWidth(80f));
                        pArr[i].playOnCommand = EditorGUILayout.Toggle(pArr[i].playOnCommand, GUILayout.MaxWidth(20f));

                        GUILayout.Label("Active", GUILayout.MaxWidth(45f));
                        pArr[i].active = EditorGUILayout.Toggle(pArr[i].active, GUILayout.MaxWidth(20f));
                        GUILayout.EndHorizontal();
                        GUILayout.Space(15f);
                    }
                }
        }
    }
}
