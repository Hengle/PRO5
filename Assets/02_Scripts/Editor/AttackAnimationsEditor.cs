using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Enemy
{
    [CustomEditor(typeof(CloseCombatAttacks), true)]
    public class AttackAnimationsEditor : Editor
    {
        bool remove = false;
        int at = 0;
        float frame = 0;

        int count;
        bool foldout;
        Enemy.EffectType effect;
        public override void OnInspectorGUI()
        {
            // DrawDefaultInspector();
            CloseCombatAttacks t = (CloseCombatAttacks)target;
            // EditorGUILayout.ObjectField(t, typeof(CloseCombatAttacks), false);

            foldout = EditorGUILayout.Foldout(foldout, "Animations");

            if (foldout)
            {
                if (t.attackAnimations.Count != 0)
                {
                    count = 0;
                    foreach (Enemy.AttackAnimations anim in t.attackAnimations)
                    {
                        string name = anim.clip != null ? anim.clip.name : "no clip";

                        GUILayout.Label("Animation " + count + " | " + name, EditorStyles.boldLabel);
                        anim.clip = (AnimationClip)EditorGUILayout.ObjectField("Clip", anim.clip, typeof(AnimationClip), true, GUILayout.MinWidth(200f));
                        anim.damageFrameStart = EditorGUILayout.FloatField("Damage Frame Start", anim.damageFrameStart, GUILayout.MinWidth(180f));
                        anim.damageFrameEnd = EditorGUILayout.FloatField("Damage Frame End", anim.damageFrameEnd, GUILayout.MinWidth(180f));
                        anim.attRange = EditorGUILayout.FloatField("Attack Range", anim.attRange, GUILayout.MinWidth(180f));
                        anim.attackWidth = EditorGUILayout.FloatField("Attack Width", anim.attackWidth, GUILayout.MinWidth(180f));
                        GUILayout.Space(10f);

                        // GUILayout.Label("Effects");
                        // if (anim.effects.Count != 0)
                        //     foreach (Enemy.EffectsFrameContainer entry in anim.effects)
                        //     {
                        //         GUILayout.BeginHorizontal();
                        //         EditorGUILayout.LabelField(entry.type.ToString());
                        //         EditorGUILayout.LabelField(entry.frame.ToString());
                        //         GUILayout.EndHorizontal();
                        //     }
                        // GUILayout.Space(10f);

                        // effect = (Enemy.EffectType)EditorGUILayout.EnumPopup("Effect Type", effect, GUILayout.MinWidth(200f), GUILayout.MaxWidth(250f));
                        // frame = EditorGUILayout.FloatField("Frame", frame, GUILayout.MinWidth(180f), GUILayout.MaxWidth(200f));

                        GUILayout.Space(10f);
                        // if (GUILayout.Button("Add Effect to Animation"))
                        // {
                        //     anim.effects.Add(new Enemy.EffectsFrameContainer(effect, frame));
                        // }

                        // if (GUILayout.Button("Remove Effect"))
                        // {
                        //     if (anim.effects.Find(x => x.type.Equals(effect)) != null)
                        //         anim.effects.RemoveAt(anim.effects.FindIndex(0, anim.effects.Count, x => x.type.Equals(effect)));
                        // }

                        count++;

                        GUILayout.Space(10f);
                    }
                }

                EditorGUILayout.Separator();
                GUILayout.Label("___________________________________________________");
                EditorGUILayout.Separator();

                if (GUILayout.Button("Add attack animation"))
                {
                    t.attackAnimations.Add(new Enemy.AttackAnimations());
                }

                if (!remove)
                {
                    if (GUILayout.Button("Remove attack animation"))
                    {
                        remove = true;
                    }
                }
                else
                {
                    at = EditorGUILayout.IntField("Remove at: ", at);
                    if (GUILayout.Button("Remove"))
                    {
                        t.attackAnimations.RemoveAt(at);
                        remove = false;
                    }
                    if (GUILayout.Button("Cancel"))
                        remove = false;
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
