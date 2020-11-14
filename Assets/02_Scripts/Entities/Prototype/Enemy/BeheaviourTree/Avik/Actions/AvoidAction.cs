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

        [InParam("aiManager")]
        public AIManager aiManager;

        public AISteering steering = new AISteering();
        public override TaskStatus OnUpdate()
        {
            if (!IsHeadingForCollision())
                return TaskStatus.COMPLETED;


            agent.destination = steering.AvoidanceSteering(gameObject.transform.forward, aiManager, gameObject);


            Vector3 moveTo = gameObject.transform.forward * (enemyBody.statistics.GetStatValue(StatName.Speed) * enemyBody.statistics.GetMultValue(MultiplierName.speed)) * Time.deltaTime;

            agent.Move(moveTo);

            return TaskStatus.RUNNING;
        }

        bool IsHeadingForCollision()
        {
            Vector3 dir = Vector3.zero;

            if ((gameObject.transform.position - aiManager.playerTarget.position).sqrMagnitude < Mathf.Pow(aiManager.avoidDistance, 2))
            {
                dir = aiManager.playerTarget.position - gameObject.transform.position;
                dir = dir.normalized;
                RaycastHit hit;
                if (Physics.SphereCast(gameObject.transform.position, 0.5f, dir, out hit, 3f, aiManager.enemyMask))
                {
                    Debug.DrawRay(gameObject.transform.position, dir, Color.yellow);

                    return true;
                }
            }
            return false;
        }
    }

}


