using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// Keeps reference to all Effects (SFX, VFX, Particle Systems) on an Object and is used for playing effects 
/// </summary>
public class EffectManager : MonoBehaviour
{
    [HideInInspector] public List<SoundEffectController> soundFX;
    [HideInInspector] public List<ParticleEffectController> particleFX;
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
}
