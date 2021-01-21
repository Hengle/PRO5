using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Simple Container for various effects. Mainly used for scripting effects for animations.
/// </summary>
[System.Serializable]
public abstract class EffectContainer
{
    public EffectType type;
    public float frame;
    public abstract void PlayEffect();
    public abstract bool IsOnCollision();
    public abstract void SetActive(bool active);
}

[System.Serializable]
public class SoundEffectControllerContainer : EffectContainer
{
    public SoundEffectController soundEffect;
    public override void PlayEffect()
    {
        soundEffect.PlayEffect();
    }
    public override void SetActive(bool active)
    {
        soundEffect.active = active;
    }
    public override bool IsOnCollision()
    {
        return soundEffect.playOnCollision;
    }
}

[System.Serializable]
public class EventEmitterContainer : EffectContainer
{
    public string name;
    public FMODUnity.StudioEventEmitter eventEmitter;
    public override bool IsOnCollision()
    {
        return false;   
    }

    public override void PlayEffect()
    {
        eventEmitter.Play();
    }

    public override void SetActive(bool active)
    {
        
    }
}

[System.Serializable]
public class ParticleEffectContainer : EffectContainer
{
    public ParticleEffectController particleSystem;
    public override void PlayEffect()
    {
        particleSystem.PlayEffect();
    }
    public override void SetActive(bool active)
    {
        particleSystem.active = active;
    }
    public override bool IsOnCollision()
    {
        return particleSystem.playOnCollision;
    }
}

[System.Serializable]
public class VFXContainer : EffectContainer
{
    public override void PlayEffect()
    {

    }
    public override void SetActive(bool active)
    {

    }
    public override bool IsOnCollision()
    {
        return false;
    }
}

public enum EffectType
{
    ParticleEffect,
    VFX,
    SoundEffect
}