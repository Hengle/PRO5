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

        public AISteering steering = new AISteering();
        public override TaskStatus OnUpdate()
        {
            if (!IsHeadingForCollision())
                return TaskStatus.COMPLETED;


            agent.destination = steering.AvoidanceSteering(enemyBody.transform.forward, enemyBody);


            Vector3 moveTo = enemyBody.transform.forward * (enemyBody.GetStatValue(StatName.Speed) * enemyBody.GetMultValue(MultiplierName.speed)) * Time.deltaTime;

            agent.Move(moveTo);

            return TaskStatus.RUNNING;
        }

        bool IsHeadingForCollision()
        {
            Vector3 dir = Vector3.zero;

            if ((enemyBody.transform.position - enemyBody.aiManager.playerTarget.position).sqrMagnitude < Mathf.Pow(enemyBody.aiManager.avoidDistance, 2))
            {
                dir = enemyBody.aiManager.playerTarget.position - enemyBody.transform.position;
                dir = dir.normalized;
                RaycastHit hit;
                if (Physics.SphereCast(enemyBody.transform.position, 0.5f, dir, out hit, 3f, enemyBody.aiManager.enemyMask))
                {
                    Debug.DrawRay(enemyBody.transform.position, dir, Color.yellow);

                    return true;
                }
            }
            return false;
        }
    }

}


