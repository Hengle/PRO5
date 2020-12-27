using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    public Animator animator;

    public void PlayAttackAnim(AnimationClip clip)
    {
        animator.Play(clip.name);
    }

    public void AnimTransition(AnimationClip clip, AnimationClip clip2, float transition)
    {

    }

    public void CancleAnim(Animation clip)
    {
        animator.CrossFade(clip.name, 0.1f);
    }

    public void CancleAnim()
    {
        animator.SetTrigger("Cancel");
    }

}

