using UnityEngine;
public enum AnimatorStrings
{
    undefined,

    animSpeed,

    comboTrigger,

    attacknr,

    cancel,

    stop
}

public abstract class IEnemyActions : MonoBehaviour
{
    public abstract void Init();
    public abstract void Attack(int i = -1, bool combo = false);
    public abstract void CancelAttack();
    public abstract void StopAttack();

    public abstract void Walk();
    public abstract void StopWalking();
    public abstract void Stunned();

    public abstract bool CheckIsAttacking();

    public abstract float GetAttackCountdown();

    public abstract Animator GetAnimator();
}
