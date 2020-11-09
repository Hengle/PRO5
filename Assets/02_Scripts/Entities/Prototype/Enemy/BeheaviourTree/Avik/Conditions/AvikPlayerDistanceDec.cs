using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;


namespace BBUnity.Conditions
{
    [Condition("EnemyBehaviour/CheckTargetDistance")]
    [Help("Checks the Distance to the target")]
    public class AvikPlayerDistanceDec : GOCondition
    {
        [InParam("enemyBody", typeof(EnemyBody))]
        public EnemyBody enemyBody;

        //[InParam("actions", typeof(IEnemyActions))]
        //public IEnemyActions actions;

        [InParam("agent", typeof(NavMeshAgent))]
        public NavMeshAgent agent;

        [InParam("aiManager", typeof(AIManager))]
        public AIManager aiManager;

        
        public override bool Check()
        {
            return CheckForPlayer();
        }

        public bool CheckForPlayer()
        {
            return (gameObject.transform.position - aiManager.playerTarget.position).sqrMagnitude < Mathf.Pow(enemyBody.GetStatValue(StatName.Range), 2);
            // {
            // if (!actions.CheckIsAttacking())
            // {
            //     // actions.Walk();
            //     agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

            // return false;
            // }
            // else
            // {
            //     // actions.StopWalking();

            //     agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            //     return false;
            // }
            // }
            // else
            // {

            // actions.StopWalking();

            // agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            // return true;

            // }
        }

        bool CheckInFront()
        {
            Ray ray = new Ray(enemyBody.rayEmitter.position, enemyBody.transform.forward);
            return Physics.SphereCast(ray, 0.2f, enemyBody.GetStatValue(StatName.Range), LayerMask.GetMask("Player"));
        }


    }
}
