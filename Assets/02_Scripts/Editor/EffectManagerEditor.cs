using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EffectManager), true)]
public class EffectManagerEditor : Editor
{
    float frame = 0;
    bool foldout = false;
    EffectType effect;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EffectManager t = (EffectManager)target;

        foldout = EditorGUILayout.Foldout(foldout, "Effects", EditorStyles.boldFont);

        if (foldout)
        {
            if (t.soundFX == null)
                t.soundFX = new List<SoundEffectController>();

            if (t.particleFX == null)
                t.particleFX = new List<ParticleEffectController>();

            if (t.VFX == null)
                t.VFX = new List<VFXContainer>();

            GUILayout.Space(10f);
            //SFX
            GUILayout.Label("SoundFX", EditorStyles.boldLabel);

            if (t.soundFX.Count != 0)
                for (int j = 0; j < t.soundFX.Count; j++)
                {
                    GUILayout.BeginHorizontal();

                    if (t.soundFX[j] != null)
                    {
                        t.soundFX[j].effectName = EditorGUILayout.TextField(t.soundFX[j].effectName, GUILayout.MaxWidth(60f));
                        t.soundFX[j].playOnCommand = EditorGUILayout.Toggle(t.soundFX[j].playOnCommand, GUILayout.MaxWidth(40f));
                        t.soundFX[j].playOnCollision = EditorGUILayout.Toggle(t.soundFX[j].playOnCommand, GUILayout.MaxWidth(40f));
                        // t.particleFX[j].particle = (ParticleSystem)EditorGUILayout.ObjectField(t.particleFX[j].particle, typeof(ParticleSystem), true, GUILayout.MaxWidth(250f));
                    }
                    t.soundFX[j] = (SoundEffectController)EditorGUILayout.ObjectField(t.soundFX[j], typeof(SoundEffectController), true, GUILayout.MaxWidth(200));

                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                        t.soundFX.Remove(t.soundFX[j]);
                    GUILayout.EndHorizontal();
                }

            GUILayout.Space(10f);

            //PFX
            GUILayout.Label("ParticleFX", EditorStyles.boldLabel);
            if (t.particleFX.Count != 0)
                for (int j = 0; j < t.particleFX.Count; j++)
                {
                    GUILayout.BeginHorizontal();
                    if (t.particleFX[j] != null)
                    {
                        t.particleFX[j].effectName = EditorGUILayout.TextField(t.particleFX[j].effectName, GUILayout.MaxWidth(60f));
                        t.particleFX[j].playOnCommand = EditorGUILayout.Toggle(t.particleFX[j].playOnCommand, GUILayout.MaxWidth(40f));
                        t.particleFX[j].playOnCollision = EditorGUILayout.Toggle(t.particleFX[j].playOnCommand, GUILayout.MaxWidth(40f));
                        // t.particleFX[j].particle = (ParticleSystem)EditorGUILayout.ObjectField(t.particleFX[j].particle, typeof(ParticleSystem), true, GUILayout.MaxWidth(250f));
                    }
                    t.particleFX[j] = (ParticleEffectController)EditorGUILayout.ObjectField(t.particleFX[j], typeof(ParticleEffectController), true, GUILayout.MaxWidth(200f));

                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                        t.particleFX.Remove(t.particleFX[j]);
                    GUILayout.EndHorizontal();

                }

            GUILayout.Space(10f);

            GUILayout.Label("VFX", EditorStyles.boldLabel);
            if (t.VFX.Count != 0)
                for (int j = 0; j < t.VFX.Count; j++)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Name:", GUILayout.MaxWidth(60f), GUILayout.MaxWidth(50f));
                    // t.VFX[j].name = EditorGUILayout.TextField(t.VFX[j].name, GUILayout.MaxWidth(60f));
                    // t.attackAnimations[i].VFX[j].soundEffect = (AudioClip)EditorGUILayout.ObjectField(t.attackAnimations[i].VFX[j].soundEffect, typeof(AudioClip), true, GUILayout.MaxWidth(250f));
                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                        t.VFX.Remove(t.VFX[j]);
                    GUILayout.EndHorizontal();

                }

            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Effect Type: ", GUILayout.MaxWidth(80f));
            effect = (EffectType)EditorGUILayout.EnumPopup(effect, GUILayout.MaxWidth(100f));

            if (GUILayout.Button("Add Effect", GUILayout.MaxWidth(150f)))
            {
                switch (effect)
                {
                    case EffectType.SoundEffect:
                        t.soundFX.Add(null);
                        break;
                    case EffectType.ParticleEffect:
                        t.particleFX.Add(null);
                        break;
                    case EffectType.VFX:
                        VFXContainer v = new VFXContainer();
                        v.frame = -1;
                        v.type = effect;
                        t.VFX.Add(v);
                        break;
                }

            }
            GUILayout.EndHorizontal();

        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(t);
        }
    }
}


