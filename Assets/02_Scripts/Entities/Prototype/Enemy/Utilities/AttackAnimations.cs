using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


namespace Enemy
{
    [System.Serializable]
    public class AttackAnimations
    {
        public AnimationClip clip;
        public float damageFrameStart;
        public float damageFrameEnd;
        public float attRange;
        public float attackWidth;

        public List<SoundEffectControllerContainer> soundFX = new List<SoundEffectControllerContainer>();
        public List<ParticleEffectContainer> particleFX = new List<ParticleEffectContainer>();
        public List<VFXContainer> VFX;
    }
}