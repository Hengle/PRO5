using UnityEngine;
using System.Collections;
public abstract class IEnemyAttacks : MonoBehaviour
{
    protected Coroutine attackTimer;
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
