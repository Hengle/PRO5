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
        [SerializeField] public AnimationClip clip;
        // public PlayableAsset playable;
        [SerializeField] public float damageFrameStart;
        [SerializeField] public float damageFrameEnd;
        [SerializeField] public float attRange;
        [SerializeField] public float attackWidth;
        [SerializeField] public List<EffectsFrameContainer> effects = new List<EffectsFrameContainer>();
    }

    [System.Serializable]
    public class EffectsFrameContainer
    {
        [SerializeField] public EffectType type;
        [SerializeField] public float frame;
        public EffectsFrameContainer(EffectType _type, float _frame)
        {
            type = _type;
            frame = _frame;
        }
    }

}
