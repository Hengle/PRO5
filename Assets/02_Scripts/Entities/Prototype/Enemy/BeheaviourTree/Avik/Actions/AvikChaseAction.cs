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
        [InParam("aiManager")]
        public AIManager aiManager;

        [InParam("agent")]
        public NavMeshAgent agent;

        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        public override void OnStart()
        {
            agent.isStopped = false;
        }
        public override TaskStatus OnUpdate()
        {

            if ((gameObject.transform.position - aiManager.playerTarget.position).sqrMagnitude < Mathf.Pow(enemyBody.statistics.GetStatValue(StatName.Range), 2))
            {
                return TaskStatus.COMPLETED;
            }

            Move();

            LookAt();

            return TaskStatus.RUNNING;
        }

        void Move()
        {
            // if (Vector3.Distance(aiManager.playerTarget.position, enemyBody.transform.position) < 5f)
            agent.destination = aiManager.playerTarget.position;

            Vector3 moveTo = gameObject.transform.forward * (enemyBody.statistics.GetStatValue(StatName.Speed) * enemyBody.statistics.GetMultValue(MultiplierName.speed)) * Time.deltaTime;

            agent.Move(moveTo);
        }
        void LookAt()
        {
            Vector3 dir = aiManager.playerTarget.position - gameObject.transform.position;
            dir.y = 0;
            if (Vector3.Distance(aiManager.playerTarget.position, gameObject.transform.position) < 3f)
            {
                Quaternion look = Quaternion.LookRotation(dir);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, look, Time.deltaTime * enemyBody.statistics.GetStatValue(StatName.TurnSpeed));
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
