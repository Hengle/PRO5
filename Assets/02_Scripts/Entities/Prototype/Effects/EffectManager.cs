using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// Keeps reference to all Effects (SFX, VFX, Particle Systems) on an Object and is used for playing effects 
/// </summary>
public class EffectManager : MonoBehaviour
{
    [HideInInspector] [SerializeField] public List<SoundEffectController> soundFX = new List<SoundEffectController>();
    [HideInInspector] [SerializeField] public List<ParticleEffectController> particleFX = new List<ParticleEffectController>();
    [HideInInspector] public List<VFXContainer> VFX;


    //TODO: Start various Effects from here by name

    public void PlaySoundEffect(string name)
    {
        soundFX.Find(x => x.effectName.Equals(name)).PlayEffect();
    }

    public void PlayParticleEffect(string name)
    {
        soundFX.Find(x => x.effectName.Equals(name)).PlayEffect();
    }

    public void StopSoundEffect(string name)
    {
        soundFX.Find(x => x.effectName.Equals(name)).StopEffect();
    }
}
