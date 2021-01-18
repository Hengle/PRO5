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
    public override void CancelAttack()
    {
        isAttacking = false;
        canDamage = false;

        if (attackTimer != null)
            StopCoroutine(attackTimer);

        if (damageWaiter != null)
            StopCoroutine(damageWaiter);

        CancelEffects();
    }

    public override abstract void StopAttack();

    protected virtual bool DoDamage()
    {
        return false;
    }

    //Times the damage window with the provided frames from the attack animation and starts effects
    protected virtual IEnumerator AttackTimer(float startDamageFrame, float stopDamageFrame, Enemy.AttackAnimations anim, float clipLength = 0)
    {
        float start = startDamageFrame / 24;
        float end = (stopDamageFrame - startDamageFrame) / 24;

        if (anim.soundFX != null && anim.soundFX.Count != 0)
            foreach (EffectContainer effect in anim.soundFX)
                StartEffects(effect);

        if (anim.particleFX != null && anim.particleFX.Count != 0)
            foreach (EffectContainer effect in anim.particleFX)
                StartEffects(effect);

        yield return new WaitForSeconds(start);

        canDamage = true;

        damageWaiter = StartCoroutine(DamageTimer(end));
        yield return damageWaiter;

        canDamage = false;

        yield return new WaitForSeconds(anim.clip.length - end);

        isAttacking = false;

        DeactivateCollisionEffects();
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
