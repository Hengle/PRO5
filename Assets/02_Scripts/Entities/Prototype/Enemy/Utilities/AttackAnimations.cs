using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public enum EffectType
{
    Trail,
    Explosion
}
namespace Enemy
{
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
        public List<Enemy.EffectsFrameContainer> effects = new List<EffectsFrameContainer>();
        public float clipLength => clip != null ? clip.length : 0;
    }

}
