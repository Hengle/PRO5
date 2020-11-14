using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class AttackAnimations
    {
        public AnimationClip clip;
        public string animationName;
        public float damageFrameStart;
        public float damageFrameEnd;
        public float clipLength => clip != null ? clip.length : 0;
    }
}
