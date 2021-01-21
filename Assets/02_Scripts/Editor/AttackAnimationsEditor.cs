using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IEnemyAttacks), true)]
[CanEditMultipleObjects]
public class AttackAnimationsEditor : Editor
{
    float frame = 0;
    bool foldout = true;
    EffectType effect;

    List<bool> animationFoldout = new List<bool>();
    List<bool> effectFoldout = new List<bool>();
    bool doOnce = true;
    CompareEffectContainer flaotC = new CompareEffectContainer();
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        IEnemyAttacks t = (IEnemyAttacks)target;

        if (animationFoldout.Count == 0)
            doOnce = true;

        foldout = EditorGUILayout.Foldout(foldout, "Animations", EditorStyles.boldFont);

        if (doOnce)
        {
            animationFoldout = new List<bool>();
            effectFoldout = new List<bool>();
            foreach (Enemy.AttackAnimations anim in t.attackAnimations)
            {
                animationFoldout.Add(false);
                effectFoldout.Add(false);
            }

            doOnce = false;
        }

        if (foldout)
        {
            if (t.attackAnimations.Count != 0)
            {
                for (int i = 0; i < t.attackAnimations.Count; i++)
                {
                    if (t.attackAnimations[i].soundFX == null)
                        t.attackAnimations[i].soundFX = new List<SoundEffectControllerContainer>();

                    if (t.attackAnimations[i].particleFX == null)
                        t.attackAnimations[i].particleFX = new List<ParticleEffectContainer>();

                    if (t.attackAnimations[i].VFX == null)
                        t.attackAnimations[i].VFX = new List<VFXContainer>();

                    string name = t.attackAnimations[i].clip != null ? t.attackAnimations[i].clip.name : "no clip";
                    GUILayout.BeginHorizontal();

                    animationFoldout[i] = EditorGUILayout.Foldout(animationFoldout[i], "Animation " + i + " | " + name);

                    if (GUILayout.Button("Remove"))
                    {
                        t.attackAnimations.Remove(t.attackAnimations[i]);
                        animationFoldout.RemoveAt(i);
                        effectFoldout.RemoveAt(i);
                    }

                    GUILayout.EndHorizontal();

                    if (animationFoldout[i])
                    {
                        t.attackAnimations[i].clip = (AnimationClip)EditorGUILayout.ObjectField("Clip", t.attackAnimations[i].clip, typeof(AnimationClip), true, GUILayout.MinWidth(200f));
                        t.attackAnimations[i].damageFrameStart = EditorGUILayout.FloatField("Damage Frame Start", t.attackAnimations[i].damageFrameStart, GUILayout.MinWidth(180f));
                        t.attackAnimations[i].damageFrameEnd = EditorGUILayout.FloatField("Damage Frame End", t.attackAnimations[i].damageFrameEnd, GUILayout.MinWidth(180f));
                        t.attackAnimations[i].attRange = EditorGUILayout.FloatField("Attack Range", t.attackAnimations[i].attRange, GUILayout.MinWidth(180f));
                        t.attackAnimations[i].attackWidth = EditorGUILayout.FloatField("Attack Width", t.attackAnimations[i].attackWidth, GUILayout.MinWidth(180f));

                        GUILayout.Space(10f);

                        effectFoldout[i] = EditorGUILayout.Foldout(effectFoldout[i], "Effects");

                        if (effectFoldout[i])
                        {
                            GUILayout.Label("SoundFX", EditorStyles.boldLabel);

                            if (t.attackAnimations[i].soundFX.Count != 0)
                                for (int j = 0; j < t.attackAnimations[i].soundFX.Count; j++)
                                {
                                    
                                    GUILayout.BeginHorizontal();
                                    if (t.attackAnimations[i].soundFX[j].soundEffect != null)
                                    {
                                        t.attackAnimations[i].soundFX[j].soundEffect.effectName = EditorGUILayout.TextField(t.attackAnimations[i].soundFX[j].soundEffect.effectName, GUILayout.MaxWidth(100f));
                                        EditorGUILayout.LabelField("On Command:", GUILayout.MaxWidth(60f));
                                        t.attackAnimations[i].soundFX[j].soundEffect.playOnCommand = EditorGUILayout.Toggle(t.attackAnimations[i].soundFX[j].soundEffect.playOnCommand, GUILayout.MaxWidth(40f));
                                        EditorGUILayout.LabelField("On Collision:", GUILayout.MaxWidth(60f));
                                        t.attackAnimations[i].soundFX[j].soundEffect.playOnCollision = EditorGUILayout.Toggle(t.attackAnimations[i].soundFX[j].soundEffect.playOnCollision, GUILayout.MaxWidth(40f));
                                        if (t.attackAnimations[i].soundFX[j].soundEffect.playOnCommand)
                                        {
                                            EditorGUILayout.LabelField("At Frame:", GUILayout.MaxWidth(50f));
                                            t.attackAnimations[i].soundFX[j].frame = EditorGUILayout.FloatField(t.attackAnimations[i].soundFX[j].frame, GUILayout.MaxWidth(60f));
                                        }
                                        if (t.attackAnimations[i].soundFX[j].soundEffect.playOnCollision)
                                        {
                                            t.attackAnimations[i].soundFX[j].frame = 99;
                                            t.attackAnimations[i].soundFX[j].SetActive(false);
                                        }
                                    }

                                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                                    {
                                        if (t.attackAnimations[i].soundFX[j].soundEffect != null)
                                            t.attackAnimations[i].soundFX[j].SetActive(true);
                                        t.attackAnimations[i].soundFX.Remove(t.attackAnimations[i].soundFX[j]);
                                    }
                                    GUILayout.EndHorizontal();
                                    t.attackAnimations[i].soundFX[j].soundEffect = (SoundEffectController)EditorGUILayout.ObjectField(t.attackAnimations[i].soundFX[j].soundEffect, typeof(SoundEffectController), true, GUILayout.MaxWidth(250f));
                                }

                            GUILayout.Space(10f);

                            GUILayout.Label("ParticleFX", EditorStyles.boldLabel);
                            if (t.attackAnimations[i].particleFX.Count != 0)
                                for (int j = 0; j < t.attackAnimations[i].particleFX.Count; j++)
                                {
                                    GUILayout.BeginHorizontal();
                                    if (t.attackAnimations[i].particleFX[j].particleSystem != null)
                                    {
                                        t.attackAnimations[i].particleFX[j].particleSystem.effectName = EditorGUILayout.TextField(t.attackAnimations[i].particleFX[j].particleSystem.effectName, GUILayout.MaxWidth(100f));
                                        EditorGUILayout.LabelField("On Command:", GUILayout.MaxWidth(60f));
                                        t.attackAnimations[i].particleFX[j].particleSystem.playOnCommand = EditorGUILayout.Toggle(t.attackAnimations[i].particleFX[j].particleSystem.playOnCommand, GUILayout.MaxWidth(40f));
                                        EditorGUILayout.LabelField("On Collision:", GUILayout.MaxWidth(60f));
                                        t.attackAnimations[i].particleFX[j].particleSystem.playOnCollision = EditorGUILayout.Toggle(t.attackAnimations[i].particleFX[j].particleSystem.playOnCollision, GUILayout.MaxWidth(40f));
                                        if (t.attackAnimations[i].particleFX[j].particleSystem.playOnCommand)
                                        {
                                            EditorGUILayout.LabelField("At Frame:", GUILayout.MaxWidth(60f), GUILayout.MaxWidth(50f));
                                            t.attackAnimations[i].particleFX[j].frame = EditorGUILayout.FloatField(t.attackAnimations[i].particleFX[j].frame, GUILayout.MaxWidth(60f));
                                        }
                                        if (t.attackAnimations[i].particleFX[j].particleSystem.playOnCollision)
                                        {
                                            t.attackAnimations[i].particleFX[j].frame = 99;
                                            t.attackAnimations[i].particleFX[j].SetActive(false);
                                        }
                                    }
                                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                                    {
                                        if (t.attackAnimations[i].particleFX[j].particleSystem != null)
                                            t.attackAnimations[i].particleFX[j].SetActive(true);
                                        t.attackAnimations[i].particleFX.Remove(t.attackAnimations[i].particleFX[j]);
                                    }
                                    GUILayout.EndHorizontal();
                                    t.attackAnimations[i].particleFX[j].particleSystem = (ParticleEffectController)EditorGUILayout.ObjectField(t.attackAnimations[i].particleFX[j].particleSystem, typeof(ParticleEffectController), true, GUILayout.MaxWidth(250f));
                                }

                            GUILayout.Space(10f);

                            GUILayout.Label("VFX", EditorStyles.boldLabel);
                            if (t.attackAnimations[i].VFX.Count != 0)
                                for (int j = 0; j < t.attackAnimations[i].VFX.Count; j++)
                                {
                                    GUILayout.BeginHorizontal();

                                    // t.attackAnimations[i].VFX[j].soundEffect = (AudioClip)EditorGUILayout.ObjectField(t.attackAnimations[i].VFX[j].soundEffect, typeof(AudioClip), true, GUILayout.MaxWidth(250f));

                                    EditorGUILayout.LabelField("At Frame:", GUILayout.MaxWidth(60f), GUILayout.MaxWidth(50f));
                                    t.attackAnimations[i].VFX[j].frame = EditorGUILayout.FloatField(t.attackAnimations[i].VFX[j].frame, GUILayout.MaxWidth(60f));

                                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                                        t.attackAnimations[i].VFX.Remove(t.attackAnimations[i].VFX[j]);
                                    GUILayout.EndHorizontal();

                                }

                            GUILayout.Space(10f);
                            GUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("Effect Type: ", GUILayout.MaxWidth(80f));
                            effect = (EffectType)EditorGUILayout.EnumPopup(effect, GUILayout.MaxWidth(100f));
                            EditorGUILayout.LabelField("At Frame: ", GUILayout.MaxWidth(80f));
                            frame = EditorGUILayout.FloatField(frame, GUILayout.MaxWidth(50f));


                            if (GUILayout.Button("Add Effect to Animation", GUILayout.MaxWidth(150f)))
                            {
                                switch (effect)
                                {
                                    case EffectType.SoundEffect:
                                        SoundEffectControllerContainer s = new SoundEffectControllerContainer();
                                        s.frame = frame;
                                        s.type = effect;
                                        t.attackAnimations[i].soundFX.Add(s);
                                        break;
                                    case EffectType.ParticleEffect:
                                        ParticleEffectContainer p = new ParticleEffectContainer();
                                        p.frame = frame;
                                        p.type = effect;
                                        t.attackAnimations[i].particleFX.Add(p);
                                        break;
                                    case EffectType.VFX:
                                        VFXContainer v = new VFXContainer();
                                        v.frame = frame;
                                        v.type = effect;
                                        t.attackAnimations[i].VFX.Add(v);
                                        break;
                                }

                            }
                            GUILayout.EndHorizontal();
                        }
                    }

                    GUILayout.Space(10f);
                    GUILayout.Label("___________________________________________________");
                    GUILayout.Space(10f);
                }
            }
            EditorGUILayout.Separator();

            if (GUILayout.Button("Add attack animation", GUILayout.MaxWidth(150f)))
            {
                t.attackAnimations.Add(new Enemy.AttackAnimations());
                animationFoldout.Add(false);
                effectFoldout.Add(false);
            }
        }

        if (GUI.changed)
        {
            for (int i = 0; i < t.attackAnimations.Count; i++)
            {
                if (t.attackAnimations[i].particleFX.Count != 0)
                    t.attackAnimations[i].particleFX.Sort(flaotC);
                if (t.attackAnimations[i].soundFX.Count != 0)
                    t.attackAnimations[i].soundFX.Sort(flaotC);
            }
            EditorUtility.SetDirty(t);
        }
    }
}

public class CompareEffectContainer : IComparer<EffectContainer>
{
    int IComparer<EffectContainer>.Compare(EffectContainer x, EffectContainer y)
    {
        // SoundEffectContainer s = null;
        // SoundEffectContainer s1 = null;
        // ParticleEffectContainer p = null;
        // ParticleEffectContainer p1 = null;

        // if (x is SoundEffectContainer)
        //     s = (SoundEffectContainer)x;
        // if (x is ParticleEffectContainer)
        //     p = (ParticleEffectContainer)x;

        // if (x is SoundEffectContainer)
        //     s1 = (SoundEffectContainer)y;
        // if (x is ParticleEffectContainer)
        //     p1 = (ParticleEffectContainer)y;

        // if (s != null && s.soundEffect.playOnCollision || p != null && p.particleSystem.playOnCollision)
        //     return 0;
        // if (s1 != null && s1.soundEffect.playOnCollision || p1 != null && p1.particleSystem.playOnCollision)
        //     return 1;

        return x.frame.CompareTo(y.frame);
    }
}
