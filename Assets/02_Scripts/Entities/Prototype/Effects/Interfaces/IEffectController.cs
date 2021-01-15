using UnityEngine;

public abstract class IEffectController : MonoBehaviour
{
    public string effectName;
    public bool playOnCollision;
    public bool playOnCommand;

    public bool active = true;

    public abstract void PlayEffect();
    public abstract void StopEffect();
}
