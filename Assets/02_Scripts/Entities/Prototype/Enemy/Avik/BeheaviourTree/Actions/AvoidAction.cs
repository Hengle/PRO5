using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/AvoidAction")]
    [Help("Goes out of the way of stuff")]
    public class AvoidAction : GOAction
    {
        [InParam("agent")]
        public NavMeshAgent agent;

        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        [InParam("stats")]
        public EnemyStatistics stats;

        AIUtilities utilities;

        public AISteering steering;

        public override void OnStart()
        {
            agent.isStopped = false;
            
            if (utilities != null)
                utilities = ScriptCollection.GetScript<AIUtilities>();
            if (steering == null)
                steering = new AISteering(stats, enemyBody);
        }

        public override TaskStatus OnUpdate()
        {
            if (!IsHeadingForCollision())
                return TaskStatus.COMPLETED;


            agent.destination = steering.AvoidanceSteering(gameObject.transform.forward);


            Vector3 moveTo = gameObject.transform.forward * (stats.GetStatValue(StatName.Speed) * stats.GetMultValue(MultiplierName.speed)) * Time.deltaTime;

            agent.Move(moveTo);

            return TaskStatus.RUNNING;
        }

        bool IsHeadingForCollision()
        {
            Vector3 dir = Vector3.zero;

            if (utilities.IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, enemyBody.aiManager.avoidDistance))
            {
                dir = enemyBody.aiManager.playerTarget.position - gameObject.transform.position;
                dir = dir.normalized;
                RaycastHit hit;
                if (Physics.SphereCast(gameObject.transform.position, 0.5f, dir, out hit, 3f, enemyBody.aiManager.enemyMask))
                {
                    Debug.DrawRay(gameObject.transform.position, dir, Color.yellow);

                    return true;
                }
            }
            return false;
        }
    }

}


