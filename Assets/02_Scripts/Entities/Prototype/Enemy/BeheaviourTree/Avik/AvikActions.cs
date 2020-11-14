using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AvikActions : IEnemyActions
{
    public AvikAnimatorHook anim;
    float extraComboTime = 1f;

    public NavMeshAgent agent;
    public BehaviorExecutor executor;
    public EnemyBody enemyBody => GetComponent<EnemyBody>();
    // public ParticleSystem stunParticle;
    public void Start()
    {
        anim = (AvikAnimatorHook)GetComponent<Enemy.AnimatorHook>();
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
}
