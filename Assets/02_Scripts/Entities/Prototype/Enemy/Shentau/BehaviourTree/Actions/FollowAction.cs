using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("Follow Action")]
    public class FollowAction : GOAction
    {
        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        [InParam("EnemySatistics")]
        public EnemyStatistics stats;

        [InParam("NavMeshAgent")]
        public NavMeshAgent agent;

        [InParam("move points")]
        AIUtilities.RandomPointLocator points;

        Vector3 currentMovementpoint;
        AIUtilities utilities;

        bool isInit;
        public bool OnInit()
        {
            utilities = ScriptCollection.GetScript<AIUtilities>();
            return true;
        }

        public override void OnStart()
        {
            if (!isInit)
                isInit = OnInit();

            GetNewPoint();
        }

        public override TaskStatus OnUpdate()
        {
            if (utilities.IsInRange(currentMovementpoint, gameObject.transform.position, 1f))
            {
                if (!GetNewPoint())
                {
                    return TaskStatus.COMPLETED;
                }
            }

            utilities.MoveNavMeshAgent(agent, currentMovementpoint, gameObject.transform.forward, 2);
            // utilities.MoveNavMeshAgent(agent, enemyBody.aiManager.playerTarget.transform.position, gameObject.transform.forward, 3);

            return TaskStatus.RUNNING;
        }

        public bool GetNewPoint()
        {
            for (int i = 0; i < points.movePoints.Count; i++)
            {
                if (points.movePoints[i] != null)
                {
                    currentMovementpoint = points.movePoints[i];
                    points.movePoints.Remove(points.movePoints[i]);
                    return true;
                }
            }
            return false;
        }
    }
}