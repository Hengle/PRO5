using System.Collections;
using UnityEngine;

public class AvikAttack : CloseCombatAttacks
{
    public AvikAnimatorHook anim => (AvikAnimatorHook)GetComponent<AnimatorHook>();
    public override void Attack()
    {
        actions.isAttacking = true;

        enemyBody.hitBox.center = new Vector3(enemyBody.hitBox.center.x, enemyBody.hitBox.center.y, attackAnimations[0].attRange / 2);
        enemyBody.hitBox.size = new Vector3(attackAnimations[0].attackWidth != 0 ? attackAnimations[0].attackWidth : enemyBody.hitBox.size.x,
                                            enemyBody.hitBox.size.y,
                                            attackAnimations[0].attRange);

        anim.StartAttackAnim(attackAnimations[0].animationName, attackAnimations[0].clip);

        attackTimer = StartCoroutine(AttackTimer(attackAnimations[0].damageFrameStart,
                                                attackAnimations[0].damageFrameEnd,
                                                attackAnimations[0].clipLength));
    }

    public override void CancelAttack()
    {
        actions.isAttacking = false;
        actions.canDamage = false;
        if (attackTimer != null)
            StopCoroutine(attackTimer);
    }

    public override void StopAttack()
    {

    }

    public override bool DoDamage()
    {
        if (actions.canDamage & enemyBody.playerDetector.player != null)
        {
            MyEventSystem.instance.OnAttack(enemyBody.playerDetector.player.GetComponent<IHasHealth>(), stats.GetStatValue(StatName.BaseDmg));
            return true;
        }
        else
        {
            return false;
        }
    }
}
