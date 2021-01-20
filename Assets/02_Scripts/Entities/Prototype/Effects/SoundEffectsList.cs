using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsList : SoundEffectController
{
    

    [Tooltip("Type in the paths to the event. Doesn't require 'event:/'")]
    public List<string> effectlist = new List<string>();

    public string ev;
    public override void PlayEffect(bool random, string name = null)
    {
        if (random)
        {
            int r = Random.Range(0, effectlist.Count);
            soundEffect.ChangeEvent(eventBaseDir + effectlist[r]);
            soundEffect.Play();
        }
        else
        {
            string s = effectlist.Find(x => x.Equals(name));
            soundEffect.ChangeEvent(eventBaseDir + name);
            soundEffect.Play();
        }
    }

    public override void StopEffect()
    {
        soundEffect.Stop();
    }
}

