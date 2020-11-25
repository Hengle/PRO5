using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class AvikAnimatorHook : AnimatorHook
{
    // public PlayableDirector director;
    public override void StartAttackAnim(string attack, AnimationClip clip = null)
    {
        animator.Play(clip.name);

        Debug.Log("animation Started");
    }
}
