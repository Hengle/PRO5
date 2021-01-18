using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class SoundEffectController : IEffectController
{
    public StudioEventEmitter soundEffect;

    public override void PlayEffect()
    {
        if (playOnCommand)
            soundEffect.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playOnCollision && active)
            soundEffect.Play();
    }

    public override void StopEffect()
    {
        soundEffect.Stop();
    }

    public virtual void PlayEffect(bool random, string name = null)
    {

    }
}
