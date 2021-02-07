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
    public List<GameObject> toInstantiate = new List<GameObject>();
    [HideInInspector] public List<VFXContainer> VFX;


    //TODO: Start various Effects from here by name

    public void PlaySoundEffect(string name)
    {
        if (soundFX.Exists(x => x.effectName.Equals(name)))
            soundFX.Find(x => x.effectName.Equals(name)).PlayEffect();
    }

    public void PlayParticleEffect(string name)
    {
        if (particleFX.Exists(x => x.effectName.Equals(name)))
            particleFX.Find(x => x.effectName.Equals(name)).PlayEffect();
    }

    public void StopSoundEffect(string name)
    {
        if (soundFX.Exists(x => x.effectName.Equals(name)))
            soundFX.Find(x => x.effectName.Equals(name)).StopEffect();
    }

    public void StopParticleEffect(string name)
    {
        if (particleFX.Exists(x => x.effectName.Equals(name)))
            particleFX.Find(x => x.effectName.Equals(name)).StopEffect();
    }

    public void SpawnParticleffecT(string name)
    {
        if (toInstantiate.Exists(x => x.GetComponent<ParticleEffectController>().effectName.Equals(name)))
        {
            var p = toInstantiate.Find(x => x.GetComponent<ParticleEffectController>().effectName.Equals(name));
            Instantiate(p, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }
    }
}
