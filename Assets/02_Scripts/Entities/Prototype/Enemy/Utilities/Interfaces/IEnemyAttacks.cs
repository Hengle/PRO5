using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class IEnemyAttacks : MonoBehaviour
{
    public EnemyBody enemyBody => GetComponent<EnemyBody>();
    public EnemyActions actions => GetComponent<EnemyActions>();
    public EnemyStatistics stats => GetComponent<EnemyStatistics>();
    [SerializeField] public Enemy.AttackAnimations[] attackAnimations;

    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract void StopAttack();
}
