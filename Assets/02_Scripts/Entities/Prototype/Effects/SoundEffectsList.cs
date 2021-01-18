using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SoundEffectsList : SoundEffectController
{
    string eventBaseDir = "event:/";

    [Tooltip("Type in the paths to the event. Doesn't require 'event:/'")]
    public List<string> effectlist = new List<string>();
    
    public string ev;
    public override void PlayEffect(bool random, string name = null)
    {
        if (random)
        {
            int r = Random.Range(0, effectlist.Count);
            soundEffect.Event = eventBaseDir + effectlist[r];
            soundEffect.Play();
        }
        else
        {
            string s = effectlist.Find(x => x.Equals(name));
            soundEffect.Event = eventBaseDir + s;
            soundEffect.Play();
        }
    }

    public override void StopEffect()
    {
        soundEffect.Stop();
    }
}
