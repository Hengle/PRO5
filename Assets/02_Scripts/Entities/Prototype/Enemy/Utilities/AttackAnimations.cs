using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Enemy
{
    public enum EffectType
    {
        Trail,
        Explosion
    }

    [System.Serializable]
    public class AttackAnimations
    {
        public AnimationClip clip;
        // public PlayableAsset playable;
        public string animationName => clip.name;
        public float damageFrameStart;
        public float damageFrameEnd;
        public float attRange;
        public float attackWidth;
        public List<EffectsFrameContainer> effects = new List<EffectsFrameContainer>();
        public float clipLength => clip != null ? clip.length : 0;
    }

    [System.Serializable]
    public class EffectsFrameContainer
    {
        public EffectType type;
        public float frame;
        public EffectsFrameContainer(EffectType _type, float _frame)
        {
            type = _type;
            frame = _frame;
        }
    }

}
