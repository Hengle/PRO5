using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public abstract class AnimatorHook : MonoBehaviour
{
    protected Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void StartAttackAnim(string attack, AnimationClip clip = null)
    {

    }

    public virtual void StartAttackAnim(AnimationClip clip)
    {
        //Put animationclip into state
        //Play clip
    }

    public virtual void CancleAnim()
    {

    }

}

