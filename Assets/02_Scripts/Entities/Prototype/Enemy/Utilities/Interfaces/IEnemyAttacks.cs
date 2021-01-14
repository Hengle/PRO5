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

    protected void StartEffects(EffectContainer effects)
    {
        effectCoroutines.Add(StartCoroutine(StartEffect(effects)));
    }

    protected void CancelEffects()
    {
        if (effectCoroutines.Count != 0)
        {
            foreach (Coroutine coroutine in effectCoroutines)
            {
                StopCoroutine(coroutine);
                effectCoroutines.Remove(coroutine);
            }
        }
        DeactivateCollisionEffects();
    }

    IEnumerator StartEffect(EffectContainer effect)
    {
        float start = effect.frame / 24;
        yield return start;
        // switch (effect.type)
        // {
        //     case EffectType.SoundEffect:
        if (effect.IsOnCollision())
        {
            effect.SetActive(true);
            collisionEffects.Add(effect);
        }
        else
        {
            effect.PlayEffect();
        }
        // break;
        // case EffectType.ParticleEffect:
        // effect.PlayEffect();
        // break;
        // }
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
