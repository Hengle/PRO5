using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Enemy
{
    [CustomEditor(typeof(CloseCombatAttacks), true)]
    public class AttackAnimationsEditor : Editor
    {
        float frame = 0;
        bool foldout = false;
        Enemy.EffectType effect;

        List<bool> animationFoldout = new List<bool>();
        List<bool> effectFoldout = new List<bool>();
        bool doOnce = true;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CloseCombatAttacks t = (CloseCombatAttacks)target;
            // EditorGUILayout.ObjectField(t, typeof(CloseCombatAttacks), false);
            // if (GUILayout.Button("FindBools"))
            // {
            //     doOnce = true;
            // }

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
                            t.attackAnimations[i].soundFX = new List<SoundEffectContainer>();

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

                                        t.attackAnimations[i].soundFX[j].soundEffect = (AudioClip)EditorGUILayout.ObjectField(t.attackAnimations[i].soundFX[j].soundEffect, typeof(AudioClip), true, GUILayout.MaxWidth(250f));

                                        EditorGUILayout.LabelField("At Frame:", GUILayout.MaxWidth(60f), GUILayout.MaxWidth(50f));
                                        EditorGUILayout.LabelField(t.attackAnimations[i].soundFX[j].frame.ToString(), GUILayout.MaxWidth(60f));

                                        if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                                            t.attackAnimations[i].soundFX.Remove(t.attackAnimations[i].soundFX[j]);
                                        GUILayout.EndHorizontal();

                                    }

                                GUILayout.Space(10f);

                                GUILayout.Label("ParticleFX", EditorStyles.boldLabel);
                                if (t.attackAnimations[i].particleFX.Count != 0)
                                    for (int j = 0; j < t.attackAnimations[i].particleFX.Count; j++)
                                    {
                                        GUILayout.BeginHorizontal();

                                        t.attackAnimations[i].particleFX[j].particleSystem = (ParticleSystem)EditorGUILayout.ObjectField(t.attackAnimations[i].particleFX[j].particleSystem, typeof(ParticleSystem), true, GUILayout.MaxWidth(250f));

                                        EditorGUILayout.LabelField("At Frame:", GUILayout.MaxWidth(60f), GUILayout.MaxWidth(50f));
                                        EditorGUILayout.LabelField(t.attackAnimations[i].particleFX[j].frame.ToString(), GUILayout.MaxWidth(60f));

                                        if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                                            t.attackAnimations[i].particleFX.Remove(t.attackAnimations[i].particleFX[j]);
                                        GUILayout.EndHorizontal();

                                    }

                                GUILayout.Space(10f);
                                GUILayout.Label("VFX", EditorStyles.boldLabel);

                                if (t.attackAnimations[i].VFX.Count != 0)
                                    for (int j = 0; j < t.attackAnimations[i].VFX.Count; j++)
                                    {
                                        GUILayout.BeginHorizontal();

                                        // t.attackAnimations[i].VFX[j].soundEffect = (AudioClip)EditorGUILayout.ObjectField(t.attackAnimations[i].VFX[j].soundEffect, typeof(AudioClip), true, GUILayout.MaxWidth(250f));

                                        EditorGUILayout.LabelField("At Frame:", GUILayout.MaxWidth(60f), GUILayout.MaxWidth(50f));
                                        EditorGUILayout.LabelField(t.attackAnimations[i].VFX[j].frame.ToString(), GUILayout.MaxWidth(60f));

                                        if (GUILayout.Button("Remove", GUILayout.MaxWidth(100f)))
                                            t.attackAnimations[i].VFX.Remove(t.attackAnimations[i].VFX[j]);
                                        GUILayout.EndHorizontal();

                                    }

                                GUILayout.Space(10f);
                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("Effect Type: ", GUILayout.MaxWidth(80f));
                                effect = (Enemy.EffectType)EditorGUILayout.EnumPopup(effect, GUILayout.MaxWidth(100f));
                                EditorGUILayout.LabelField("At Frame: ", GUILayout.MaxWidth(80f));
                                frame = EditorGUILayout.FloatField(frame, GUILayout.MaxWidth(50f));


                                if (GUILayout.Button("Add Effect to Animation", GUILayout.MaxWidth(150f)))
                                {
                                    switch (effect)
                                    {
                                        case EffectType.SoundEffect:
                                            Enemy.SoundEffectContainer s = new Enemy.SoundEffectContainer();
                                            s.frame = frame;
                                            s.type = effect;
                                            t.attackAnimations[i].soundFX.Add(s);
                                            break;
                                        case EffectType.ParticleEffect:
                                            Enemy.ParticleEffectContainer p = new Enemy.ParticleEffectContainer();
                                            p.frame = frame;
                                            p.type = effect;
                                            t.attackAnimations[i].particleFX.Add(p);
                                            break;
                                        case EffectType.VFX:
                                            Enemy.VFXContainer v = new Enemy.VFXContainer();
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


                    // if (GUILayout.Button("Remove Effect"))
                    // {
                    //     if (anim.effects.Find(x => x.type.Equals(effect)) != null)
                    //         anim.effects.RemoveAt(anim.effects.FindIndex(0, anim.effects.Count, x => x.type.Equals(effect)));
                    // }

                    // GUILayout.Space(10f);
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
                EditorUtility.SetDirty(t);
        }

        void AddToArray(ref Enemy.AttackAnimations[] arr)
        {
            var old = arr;
            arr = new Enemy.AttackAnimations[arr.Length + 1];
            for (int i = 0; i < old.Length; i++)
            {
                arr[i] = old[i];
            }
            arr[arr.Length] = new Enemy.AttackAnimations();
        }

        void RemoveLast(ref Enemy.AttackAnimations[] arr)
        {
            var old = arr;
            arr = new Enemy.AttackAnimations[arr.Length - 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = old[i];
            }
        }


    }
}
