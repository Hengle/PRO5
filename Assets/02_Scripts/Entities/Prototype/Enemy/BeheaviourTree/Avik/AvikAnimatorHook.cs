using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class AvikAnimatorHook : Enemy.AnimatorHook
{
    public PlayableDirector director;
    public override void StartAttackAnim(string attack)
    {
        Debug.Log("animation Started");
    }
}
