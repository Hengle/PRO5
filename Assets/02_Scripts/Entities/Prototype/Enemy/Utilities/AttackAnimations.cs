using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class AttackAnimations
    {
        public AnimationClip clip;
        public string animationName => clip.name;
        public float damageFrameStart;
        public float damageFrameEnd;
        public float attRange;
        public float clipLength => clip != null ? clip.length : 0;
    }
}
