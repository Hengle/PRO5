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

        [InParam("MoveVector")]
        public Vector3 moveVector;

        [InParam("Agent")]
        public NavMeshAgent agent;

        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        [InParam("Stats")]
        public EnemyStatistics stats;

        public float randomRange;

        [InParam("isInRange")]
        [OutParam("isInRange")]
        public bool isInRange;

        float currentTime;

        AIUtilities utilities;
        public override void OnStart()
        {
            utilities = ScriptCollection.GetScript<AIUtilities>();
        }

        public override TaskStatus OnUpdate()
        {
            // isInRange = utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, randomRange);
            currentTime += Time.deltaTime;
            isInRange = currentTime >= 0.4f;
            if (isInRange)
            {
                currentTime = 0;
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

    }
}