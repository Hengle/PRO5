using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AvikActions : IEnemyActions
{
    public Animator animator;
    public Weapon[] weapons;
    float extraComboTime = 1f;

    public NavMeshAgent agent;
    public BehaviorExecutor executor;
    public EnemyBody enemyBody => GetComponent<EnemyBody>();
    // public ParticleSystem stunParticle;

    public override void Attack(int i = 1, bool combo = false)
    {
        switch (i)
        {
            case 1:
                ExecuteAttack(i, combo);
                break;
            case -1:
                // animator.Play("Activation");
                break;
        }

        StartCoroutine(AttackDelay());

    }

    void ExecuteAttack(int i, bool combo)
    {
        // foreach (Weapon weapon in weapons)
        //     weapon.Activate();

        executor.blackboard.SetBehaviorParam("canAttack", false);
        // controller.isAttacking = true;

        // animator.SetFloat(AnimatorStrings.animSpeed.ToString(), controller.enemyStats.GetStatValue(StatName.AttackSpeed));
        // animator.SetInteger(AnimatorStrings.attacknr.ToString(), i);
        // if (combo)
        // animator.SetTrigger(AnimatorStrings.comboTrigger.ToString());

    }

    private void SingleAttack()
    {
        animator.SetInteger(AnimatorStrings.attacknr.ToString(), 1);
    }

    public override void CancelAttack()
    {
        // animator.SetTrigger(AnimatorStrings.cancel.ToString());
    }
    public override void StopAttack()
    {
        // animator.SetTrigger(AnimatorStrings.stop.ToString());
    }
    public override void Walk()
    {
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
        // animator.SetBool("isWalking", true);
    }

    public override void StopWalking()
    {
        agent.isStopped = true;
        // animator.SetBool("isWalking", false);
    }

    public override void Stunned()
    {
        // stunParticle.Play();
    }

    public override bool CheckIsAttacking()
    {
        // if (animator.GetInteger(AnimatorStrings.attacknr.ToString()) == 0)
        // {
        //     controller.isAttacking = false;
        //     foreach (Weapon weapon in weapons)
        //         weapon.Deactivate();


        // }
        // return controller.isAttacking;
        return true;
    }

    public override float GetAttackCountdown()
    {
        return 0;
    }

    IEnumerator AttackDelay(float extra = 0)
    {
        yield return new WaitForSeconds(enemyBody.GetStatValue(StatName.AttackRate) + extra);

        executor.blackboard.SetBehaviorParam("canAttack", true);
    }

    public override Animator GetAnimator()
    {
        return null;
    }

    public override void Init()
    {

    }
}
