using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorHook : MonoBehaviour
{
    public Animator animator;

    public abstract void StartAttackAnim(AnimationClip clip);

    public abstract void CancleAnim();

}

