using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikAnimatorHook : AnimatorHook
{
    public override void CancleAnim()
    {

    }

    // public PlayableDirector director;
    public override void StartAttackAnim(AnimationClip clip)
    {
        animator.Play(clip.name);

        Debug.Log("animation Started");
    }
}
