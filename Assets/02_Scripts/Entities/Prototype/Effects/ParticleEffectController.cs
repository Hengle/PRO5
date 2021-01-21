using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used as a simple extension for Particle Systems for ease of use. Can be referenced in the EffectController.
/// </summary>

[System.Serializable]
public class ParticleEffectController : IEffectController
{
    public ParticleSystem particle;
    public override void StopEffect()
    {
        particle.Stop();
    }

    public override void PlayEffect()
    {
        if (playOnCommand)
            particle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playOnCollision && active)
            PlayEffect();
    }
}
