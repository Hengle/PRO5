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
        [InParam("InitAttack")]
        public bool initAttack;

        float currentTime;
        float rand;
        AIUtilities utilities;

        bool countTime;
        bool countReady;
        
        public override void OnStart()
        {
            utilities = ScriptCollection.GetScript<AIUtilities>();
            rand = Random.Range(0.8f, 1.5f);
        }

        public override TaskStatus OnUpdate()
        {
            if (enemyBody.playerDetector.player != null)
            {
                countTime = true;
            }

            if (countTime)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= 3f)
                {
                    countTime = false;
                    countReady = true;
                    
                    currentTime = 0;
                }
            }

            // isInRange = utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, randomRange);
            if (countReady || utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, 3f))
            {
                countReady = false;
                agent.isStopped = true;
                initAttack = true;
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