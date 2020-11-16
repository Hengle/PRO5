using System.Collections;
using UnityEngine;

public class AvikAttack : CloseCombatAttacks
{

    public BoxCollider attackCollider;
    PlayerDetector detector => attackCollider.GetComponent<PlayerDetector>();
    AvikAnimatorHook anim => (AvikAnimatorHook)GetComponent<Enemy.AnimatorHook>();
    EnemyActions actions => GetComponent<EnemyActions>();
    EnemyStatistics stats => GetComponent<EnemyStatistics>();

    Coroutine damageWaiter;
    public override void Attack()
    {
        actions.isAttacking = true;

        attackCollider.center = new Vector3(attackCollider.center.x, attackCollider.center.y, stats.GetStatValue(StatName.Range) / 2);
        attackCollider.size = new Vector3(attackCollider.size.x, attackCollider.size.y, stats.GetStatValue(StatName.Range));
        anim.StartAttackAnim(attackAnimations[0].animationName);
        attackTimer = StartCoroutine(AttackTimer(attackAnimations[0].damageFrameStart, attackAnimations[0].damageFrameEnd, attackAnimations[0].clipLength));
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
        if (actions.canDamage & detector.player != null)
        {
            MyEventSystem.instance.OnAttack(detector.player.GetComponent<IHasHealth>(), stats.GetStatValue(StatName.BaseDmg));
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
        actions.canDamage = true;

        yield return StartCoroutine(DamageTimer(end));

        actions.canDamage = false;

        if (clipLength != 0)
            yield return new WaitForSeconds(clipLength - end);

        actions.isAttacking = false;
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
