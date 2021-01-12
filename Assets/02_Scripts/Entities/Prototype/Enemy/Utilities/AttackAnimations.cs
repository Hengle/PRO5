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

        public List<SoundEffectContainer> soundFX;
        public List<ParticleEffectContainer> particleFX;
        public List<VFXContainer> VFX;
    }
}