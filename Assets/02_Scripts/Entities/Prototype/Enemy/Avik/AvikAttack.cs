using System.Collections;
using UnityEngine;

public class AvikAttack : CloseCombatAttacks
{
    public AnimatorHook anim;

    public override void Attack()
    {
        int i = GetAttackType();
        isAttacking = true;

        enemyBody.hitBox.center = new Vector3(enemyBody.hitBox.center.x, enemyBody.hitBox.center.y, attackAnimations[i].attRange / 2);

        enemyBody.hitBox.size = new Vector3(attackAnimations[i].attackWidth != 0 ? attackAnimations[i].attackWidth : enemyBody.hitBox.size.x,
                                            enemyBody.hitBox.size.y,
                                            attackAnimations[i].attRange);

        anim.PlayAttackAnim(attackAnimations[i].clip);

        attackTimer = StartCoroutine(AttackTimer(attackAnimations[i].damageFrameStart,
                                                attackAnimations[i].damageFrameEnd,
                                                attackAnimations[i].clip.length));
    }

    public override void CancelAttack()
    {
        isAttacking = false;
        canDamage = false;

        if (attackTimer != null)
            StopCoroutine(attackTimer);

        if (damageWaiter != null)
            StopCoroutine(damageWaiter);

        //Cancel animation into stun animation
    }

    public override void StopAttack()
    {
    }

    public override bool DoDamage()
    {
        if (canDamage & enemyBody.playerDetector.player != null)
        {
            MyEventSystem.instance.OnAttack(enemyBody.playerDetector.player.GetComponent<IHasHealth>(), stats.GetStatValue(StatName.BaseDmg));
            return true;
        }
        return false;
    }


    int GetAttackType()
    {
        if (ScriptCollection.GetScript<AIUtilities>().IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.Range) / 2))
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
