using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class IEnemyAttacks : MonoBehaviour
{
    public EnemyBody enemyBody => GetComponent<EnemyBody>();
    public EnemyActions actions => GetComponent<EnemyActions>();
    public EnemyStatistics stats => GetComponent<EnemyStatistics>();
    List<Coroutine> effectCoroutines = new List<Coroutine>();
    [HideInInspector] public List<Enemy.AttackAnimations> attackAnimations = new List<Enemy.AttackAnimations>();
    List<EffectContainer> collisionEffects = new List<EffectContainer>();
    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract void StopAttack();

    protected void StartEffects(EffectContainer effect)
    {
        if (effect.IsOnCollision())
        {
            effect.SetActive(true);
            collisionEffects.Add(effect);
        }
        else
        {
            effectCoroutines.Add(StartCoroutine(StartEffect(effect)));
        }
    }

    protected void CancelEffects()
    {
        if (effectCoroutines.Count != 0)
        {
            for(int i = 0; i< effectCoroutines.Count; i++)
            {
                StopCoroutine(effectCoroutines[i]);
                effectCoroutines.Remove(effectCoroutines[i]);
            }
        }
        DeactivateCollisionEffects();
    }

    IEnumerator StartEffect(EffectContainer effect)
    {
        float start = effect.frame / 24;
        yield return start;
        effect.PlayEffect();
    }

    public void DeactivateCollisionEffects()
    {
        if (collisionEffects.Count != 0)
            foreach (EffectContainer effect in collisionEffects)
            {
                effect.SetActive(false);
            }
        collisionEffects.Clear();
    }
}
