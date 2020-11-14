using UnityEngine;
public abstract class IEnemyActions : MonoBehaviour
{
    public abstract void Stunned();

    public virtual void Idle()
    {

    }
    public virtual void Walk()
    {

    }

    public virtual void StopWalking()
    {

    }
}
