using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/StunAction")]
    public class StunAction : GOAction
    {
        [InParam("agent")]
        public NavMeshAgent agent;

        [InParam("animator")]
        public Animator animator;

        [InParam("EffectManager", typeof(EffectManager))]
        public EffectManager effectManager;
        [InParam("EnemyActions")]
        public EnemyActions actions;
        public override void OnStart()
        {
            //start effects or whatever

            animator.SetBool("isStunned", true);
            effectManager.PlaySoundEffect("stun");
            effectManager.PlayParticleEffect("stun");
        }

        public override TaskStatus OnUpdate()
        {
            if (actions.isStunned || !Physics.Raycast(gameObject.transform.position, Vector3.down, 5f, LayerMask.GetMask("Floor")))
            {

                agent.isStopped = true;
                agent.enabled = false;
                return TaskStatus.RUNNING;
            }
            else
            {
                agent.isStopped = false;
                agent.enabled = true;
                return TaskStatus.COMPLETED;
            }

        }

        public override void OnAbort()
        {
            animator.SetBool("isStunned", false);
            agent.enabled = true;
            agent.isStopped = false;

            effectManager.StopSoundEffect("stun");
        }
        public override void OnEnd()
        {
            animator.SetBool("isStunned", false);
            agent.enabled = true;
            agent.isStopped = false;
            effectManager.StopSoundEffect("stun");
        }
    }
}