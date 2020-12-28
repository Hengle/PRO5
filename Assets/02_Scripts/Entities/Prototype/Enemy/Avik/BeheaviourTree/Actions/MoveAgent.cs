using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/MoveAgentTo")]
    public class MoveAgent : GOAction
    {
        [InParam("Agent")]
        public NavMeshAgent agent;

        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        [InParam("Stats")]
        public EnemyStatistics stats;

        [InParam("isInRange")]
        [OutParam("isInRange")]
        public bool isInRange;

        float currentTime;
        float rand;
        AIUtilities utilities;
        public override void OnStart()
        {
            utilities = ScriptCollection.GetScript<AIUtilities>();
            rand = Random.Range(0.8f, 1.2f);
        }

        public override TaskStatus OnUpdate()
        {
            // isInRange = utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, randomRange);
            if (utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, 3.4f))
            {
                agent.isStopped = true;
                isInRange = true;
                return TaskStatus.COMPLETED;
            }
            else
            {
                isInRange = false;
                agent.isStopped = false;
                Move();
                LookAt();
                return TaskStatus.RUNNING;
            }
            // if (!isInRange)
            // {
            //     agent.isStopped = false;
            //     currentTime += Time.deltaTime;
            //     if (currentTime >= 0.5f)
            //     {
            //         isInRange = true;
            //         currentTime = 0;
            //         agent.isStopped = true;
            //         return TaskStatus.COMPLETED;
            //     }
            // }
            // else
            // {
            //     Move();
            //     LookAt();
            // }
            // return TaskStatus.RUNNING;
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
            agent.isStopped = false;
        }

    }
}