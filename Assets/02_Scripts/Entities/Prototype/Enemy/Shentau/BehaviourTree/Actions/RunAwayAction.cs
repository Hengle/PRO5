using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("Run away Action")]
    public class RunAwayAction : GOAction
    {
        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        [InParam("EnemySatistics")]
        public EnemyStatistics stats;

        [InParam("NavMeshAgent")]
        public NavMeshAgent agent;

        [InParam("move Points")]
        AIUtilities.RandomPointLocator wayPointFinder;
        [InParam("Min search angle")]
        public float minAngle = 0.50f;

        [InParam("Max search angle")]
        public float maxAngle = 0.50f;

        [InParam("Away from wall angle")]
        public float awayFromWallAngle = 0.50f;

        [InParam("min Wall distance")]
        public float minWallDistance = 3f;

        AIUtilities utilities;
        bool isRunning = false;
        bool isInit;
        public bool OnInit()
        {
            utilities = ScriptCollection.GetScript<AIUtilities>();
            wayPointFinder = new AIUtilities.RandomPointLocator();

            return true;
        }

        public override void OnStart()
        {
            if (!isInit)
                isInit = OnInit();

            wayPointFinder.wayPoints.Clear();

            wayPointFinder.FindWaypoints(gameObject, enemyBody, minAngle, maxAngle, awayFromWallAngle, minWallDistance, false, 2);
        }

        public override TaskStatus OnUpdate()
        {
            // if (utilities.IsInRange(currentMovementpoint, gameObject.transform.position, 1f))
            // Debug.Log("Remaining distance: " + agent.remainingDistance);
            Debug.Log(isRunning);
            if (!agent.hasPath || agent.remainingDistance <= 2.5f)
            {
                if (GetNewPoint())
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        wayPointFinder.FindWaypoints(gameObject, enemyBody, minAngle, maxAngle, awayFromWallAngle, minWallDistance, true, 2, () => SetRunning());
                    }
                }
            }

            // utilities.MoveNavMeshAgent(agent, currentMovementpoint, gameObject.transform.forward, 2);

            Vector3 moveTo = gameObject.transform.forward * stats.GetStatValue(StatName.Speed) * Time.deltaTime;

            agent.Move(moveTo);
            // utilities.MoveNavMeshAgent(agent, enemyBody.aiManager.playerTarget.transform.position, gameObject.transform.forward, 3);

            return TaskStatus.RUNNING;
        }

        public bool GetNewPoint()
        {
            Debug.Log("Amount of waypoints: " + wayPointFinder.wayPoints.Count);
            for (int i = 0; i < wayPointFinder.wayPoints.Count; i++)
            {
                if (wayPointFinder.wayPoints[i] != null)
                {
                    agent.destination = wayPointFinder.wayPoints[i];
                    wayPointFinder.wayPoints.Remove(wayPointFinder.wayPoints[i]);
                    // Debug.Log("Waypoint found");
                    // return false;
                }
            }
            return wayPointFinder.wayPoints.Count <= 1;
        }

        void SetRunning()
        {
            isRunning = false;
        }
    }
}