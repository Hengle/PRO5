using System.Collections;
using UnityEngine;

public abstract class CloseCombatAttacks : MonoBehaviour, IEnemyAttacks
{
    protected Coroutine attackTimer;
    public bool isAttacking;
    public bool canAttack;
    public bool canDamage;
    public Enemy.AttackAnimations[] attackAnimations;
    public EnemyBody enemyBody => GetComponent<EnemyBody>();
    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract void StopAttack();
    public virtual bool DoDamage()
    {
        return true;
    }

    public virtual IEnumerator AttackTimer(float startDamgeFrame, float stopDamageFrame, float clipLength = 0)
    {
        yield return null;
    }

}
