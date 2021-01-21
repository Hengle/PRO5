using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public abstract class IEffectController : MonoBehaviour
{
    public string effectName = "";
    public bool playOnCollision;
    public bool playOnCommand;
    public bool loop;
    public bool active = true;
    protected Coroutine loopCoroutine;
    protected virtual IEnumerator LoopEffect()
    {
        yield return null;
    }

    public virtual void StopLoop()
    {
        StopCoroutine(loopCoroutine);
    }

    public abstract void PlayEffect();
    public abstract void StopEffect();
}
