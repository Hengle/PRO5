using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Conditions
{
    [Condition("EnemyBehaviour/StunCondition")]
    public class StunCondition : GOCondition
    {
        [InParam("EnemyActions")]
        public EnemyActions actions;

        [InParam("agent")]
        public NavMeshAgent agent;

        [InParam("effects")]
        public EffectManager effectManager;
        public override bool Check()
        {
            if (actions.isStunned)
            {
                agent.isStopped = true;
                agent.enabled = false;
                return true;
            }
            else
            {
                effectManager.StopParticleEffect("stun");
                agent.enabled = true;
                agent.isStopped = false;
                return false;
            }
        }
    }
}