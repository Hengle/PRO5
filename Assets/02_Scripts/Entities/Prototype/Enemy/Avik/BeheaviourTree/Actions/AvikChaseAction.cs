using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/Chase")]
    [Help("Chases a target")]
    public class AvikChaseAction : GOAction
    {
        [InParam("agent")]
        public NavMeshAgent agent;

        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        [InParam("stats")]
        public EnemyStatistics stats;

        AIUtilities utilities;

        public override void OnStart()
        {
            if (utilities == null)
                utilities = ScriptCollection.GetScript<AIUtilities>();

            agent.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.Range)))
            {
                return TaskStatus.COMPLETED;
            }

            Move();

            LookAt();

            return TaskStatus.RUNNING;
        }

        void Move()
        {
            utilities.MoveNavMeshAgent(agent,
                                         enemyBody.aiManager.playerTarget.position,
                                         gameObject.transform.forward,
                                         (stats.GetStatValue(StatName.Speed) * stats.GetMultValue(MultiplierName.speed)));
        }

        void LookAt()
        {
            if (Vector3.Distance(enemyBody.aiManager.playerTarget.position, gameObject.transform.position) < 5f)
            {
                utilities.LookAtTarget(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.TurnSpeed));
            }
        }

        public override void OnAbort()
        {
            agent.isStopped = true;
        }

        public override void OnEnd()
        {
            agent.isStopped = true;
        }
    }
}
