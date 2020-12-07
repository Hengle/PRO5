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
        bool foldout;
        Enemy.EffectType effect;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CloseCombatAttacks t = (CloseCombatAttacks)target;
            // EditorGUILayout.ObjectField(t, typeof(CloseCombatAttacks), false);

            foldout = EditorGUILayout.Foldout(foldout, "Animations");
            int count = 0;

            if (foldout)
            {
                
                if (t.attackAnimations.Length != 0)
                    foreach (Enemy.AttackAnimations anim in t.attackAnimations)
                    {
                        GUILayout.Label("Animation " + count, EditorStyles.boldLabel);
                        anim.clip = (AnimationClip)EditorGUILayout.ObjectField("Clip", anim.clip, typeof(AnimationClip), true, GUILayout.MinWidth(200f), GUILayout.MaxWidth(250f));
                        anim.damageFrameStart = EditorGUILayout.FloatField("Damage Frame Start", anim.damageFrameStart, GUILayout.MinWidth(150f), GUILayout.MaxWidth(180f));
                        anim.damageFrameEnd = EditorGUILayout.FloatField("Damage Frame End", anim.damageFrameEnd, GUILayout.MinWidth(150f), GUILayout.MaxWidth(180f));
                        anim.attRange = EditorGUILayout.FloatField("Attack Range", anim.attRange, GUILayout.MinWidth(150f), GUILayout.MaxWidth(180f));
                        anim.attackWidth = EditorGUILayout.FloatField("Attack Width", anim.attackWidth, GUILayout.MinWidth(150f), GUILayout.MaxWidth(180f));
                        GUILayout.Space(10f);

                        GUILayout.Label("Effects");
                        if (anim.effects.Count != 0)
                            foreach (Enemy.EffectsFrameContainer entry in anim.effects)
                            {
                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField(entry.type.ToString());
                                EditorGUILayout.LabelField(entry.frame.ToString());
                                GUILayout.EndHorizontal();
                            }
                        GUILayout.Space(10f);

                        effect = (Enemy.EffectType)EditorGUILayout.EnumPopup("Effect Type", effect, GUILayout.MinWidth(200f), GUILayout.MaxWidth(210f));
                        frame = EditorGUILayout.FloatField("Frame", frame, GUILayout.MinWidth(150f), GUILayout.MaxWidth(180f));

                        GUILayout.Space(10f);
                        if (GUILayout.Button("Add Effect to Animation"))
                        {
                            anim.effects.Add(new Enemy.EffectsFrameContainer(effect, frame));
                        }

                        if (GUILayout.Button("Remove Effect"))
                        {
                            if (anim.effects.Find(x => x.type.Equals(effect)) != null)
                                anim.effects.RemoveAt(anim.effects.FindIndex(0, anim.effects.Count, x => x.type.Equals(effect)));
                        }

                        count++;

                        GUILayout.Space(10f);
                    }

                EditorGUILayout.Separator();
                GUILayout.Label("___________________________________________________");
                EditorGUILayout.Separator();

                if (GUILayout.Button("Add Attack Animation"))
                {
                    AddToArray(ref t.attackAnimations);
                }

                if (GUILayout.Button("Remove last Attack Animation"))
                {
                    // t.attackAnimations.RemoveAt(t.attackAnimations.Count - 1);
                    RemoveLast(ref t.attackAnimations);

                }
            }


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
