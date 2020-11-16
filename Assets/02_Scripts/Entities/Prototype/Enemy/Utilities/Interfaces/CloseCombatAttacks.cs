using System.Collections;
using UnityEngine;

public abstract class CloseCombatAttacks : IEnemyAttacks
{
    public override abstract void Attack();
    public override abstract void CancelAttack();
    public override abstract void StopAttack();
    public override bool DoDamage()
    {
        return true;
    }

    public override IEnumerator AttackTimer(float startDamgeFrame, float stopDamageFrame, float clipLength = 0)
    {
        yield return null;
    }

}
