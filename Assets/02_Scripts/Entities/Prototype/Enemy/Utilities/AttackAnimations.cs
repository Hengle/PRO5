using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Enemy
{
    public enum EffectType
    {
        ParticleEffect,
        VFX,
        SoundEffect
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
        [SerializeField] public List<SoundEffectContainer> soundFX;
        [SerializeField] public List<ParticleEffectContainer> particleFX;
        [SerializeField] public List<VFXContainer> VFX;
    }

    [System.Serializable]
    public abstract class EffectsFrameContainer
    {
        [SerializeField] public EffectType type;
        [SerializeField] public float frame;
    }

    [System.Serializable]
    public class SoundEffectContainer : EffectsFrameContainer
    {
        public AudioClip soundEffect;
    }

    [System.Serializable]
    public class ParticleEffectContainer : EffectsFrameContainer
    {
        public ParticleSystem particleSystem;
    }

    [System.Serializable]
    public class VFXContainer : EffectsFrameContainer
    {
        //Add vfx reference
    }

}
