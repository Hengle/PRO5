using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class SoundEffectController : IEffectController
{
    protected string eventBaseDir = "event:/";
    public StudioEventEmitter soundEffect;
    public FMOD.Studio.EventInstance soundInstance;

    public bool createInstance;
    public string eventPath;
    private void Start()
    {
        if (createInstance)
        {
            soundInstance = FMODUnity.RuntimeManager.CreateInstance(eventBaseDir + eventPath);
        }
    }
    public override void PlayEffect()
    {
        if (playOnCommand)
            if (createInstance)
                soundInstance.start();
            else
                soundEffect.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playOnCollision && active)
            if (createInstance)
                soundInstance.start();
            else
                soundEffect.Play();
    }

    public override void StopEffect()
    {
        if (createInstance)
            soundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        else
            soundEffect.Stop();
    }

    public virtual void PlayEffect(bool random, string name = null)
    {

    }
}
