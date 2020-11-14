using System.Collections;
using UnityEngine;

public class AvikAttack : CloseCombatAttacks
{
    public BoxCollider attackCollider;
    PlayerDetector detector => attackCollider.GetComponent<PlayerDetector>();
    AvikAnimatorHook anim => (AvikAnimatorHook)GetComponent<Enemy.AnimatorHook>();

    Coroutine damageWaiter;
    public override void Attack()
    {
        isAttacking = true;

        attackCollider.center = new Vector3(attackCollider.center.x, attackCollider.center.y, enemyBody.statistics.GetStatValue(StatName.Range) / 2);
        attackCollider.size = new Vector3(attackCollider.size.x, attackCollider.size.y, enemyBody.statistics.GetStatValue(StatName.Range));
        anim.StartAttackAnim(attackAnimations[0].animationName);
        attackTimer = StartCoroutine(AttackTimer(attackAnimations[0].damageFrameStart, attackAnimations[0].damageFrameEnd, attackAnimations[0].clipLength));
    }

    public override void CancelAttack()
    {
        isAttacking = false;
        canDamage = false;
        if (attackTimer != null)
            StopCoroutine(attackTimer);
    }

    public override void StopAttack()
    {

    }

    public override bool DoDamage()
    {
        if (canDamage & detector.player != null)
        {
            MyEventSystem.instance.OnAttack(detector.player.GetComponent<IHasHealth>(), enemyBody.statistics.GetStatValue(StatName.BaseDmg));
            return true;
        }
        else
        {
            return false;
        }
    }


    public override IEnumerator AttackTimer(float startDamageFrame, float stopDamageFrame, float clipLength = 0)
    {
        float start = startDamageFrame / 24;
        float end = (stopDamageFrame - startDamageFrame) / 24;

        yield return new WaitForSeconds(start);
        canDamage = true;

        yield return StartCoroutine(DamageTimer(end));

        canDamage = false;

        if (clipLength != 0)
            yield return new WaitForSeconds(clipLength - end);

        isAttacking = false;
    }

    IEnumerator DamageTimer(float wait)
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
