using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseCombatAttacks : IEnemyAttacks
{
    protected Coroutine damageWaiter;
    protected Coroutine attackTimer;

    public bool isAttacking;
    public bool canAttack;
    public bool canDamage;

    public override abstract void Attack();
    public override abstract void CancelAttack();
    public override abstract void StopAttack();

    public virtual bool DoDamage()
    {
        return false;
    }

    //Times the damage window with the provided frames from the attack animation
    public virtual IEnumerator AttackTimer(float startDamageFrame, float stopDamageFrame, float clipLength = 0)
    {
        float start = startDamageFrame / 24;
        float end = (stopDamageFrame - startDamageFrame) / 24;

        yield return new WaitForSeconds(start);
        canDamage = true;

        damageWaiter = StartCoroutine(DamageTimer(end));
        yield return damageWaiter;

        canDamage = false;

        if (clipLength != 0)
            yield return new WaitForSeconds(clipLength - end);

        isAttacking = false;
    }

    protected virtual IEnumerator DamageTimer(float wait)
    {
        float cTime = 0;
        while (cTime <= wait)
        {
            if (DoDamage())
                yield break;

            cTime += Time.deltaTime;
            yield return null;
        }
    }
}
