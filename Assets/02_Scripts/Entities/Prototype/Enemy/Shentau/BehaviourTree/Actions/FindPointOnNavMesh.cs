using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
namespace BBUnity.Actions
{
    [Action("Find Point on Nav Mesh Action")]
    public class FindPointOnNavMesh : GOAction
    {
        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        [InParam("Min search angle")]
        public float minAngle = 0.50f;

        [InParam("Max search angle")]
        public float maxAngle = 0.50f;

        [InParam("Away from wall angle")]
        public float awayFromWallAngle = 0.50f;

        [InParam("min Wall distance")]
        public float minWallDistance = 3f;

        [InParam("agent")]
        public NavMeshAgent agent;

        [OutParam("move points")]
        [InParam("move points")]
        AIUtilities.RandomPointLocator points;

        AIUtilities utilities;
        
        bool isRunning = false;


        public override void OnStart()
        {
            if (points == null)
                points = new AIUtilities.RandomPointLocator();
            if (utilities == null)
                utilities = ScriptCollection.GetScript<AIUtilities>();
        }

        public override TaskStatus OnUpdate()
        {
            if (!isRunning && points.wayPoints.Count == 0)
            {
                isRunning = true;
                points.FindWaypointsAsync(gameObject, enemyBody, minAngle, maxAngle, awayFromWallAngle, minWallDistance, () => CanRun());
                return TaskStatus.RUNNING;
            }
            else
            {
                return TaskStatus.COMPLETED;
            }
        }

        void CanRun()
        {
            isRunning = false;
        }
        #region findpoints
        // async UniTask FindDirection()
        // {
        //     isRunning = true;
        //     float dot = 1;
        //     float dot2 = 2;
        //     random = gameObject.transform.position;
        //     basePos = gameObject.transform.position;
        //     int posSearchType = 0;

        //     Vector3 cornerDir = Vector3.zero;

        //     for (int i = 0; i < 1; i++)
        //     {
        //         bool done = false;
        //         int c = 0;
        //         while (!done || c > 1000)
        //         {
        //             maxDist = Random.Range(3f, 6f);
        //             Vector3 randominSphere = Random.insideUnitSphere;
        //             Vector3 unit = new Vector3(randominSphere.x < 1 ? randominSphere.x + 1 : randominSphere.x,
        //                                         randominSphere.y,
        //                                         randominSphere.z < 1 ? randominSphere.z + 1 : randominSphere.z) * maxDist;
        //             random = basePos + unit;

        //             int wallcheck = CheckForWalls(ref cornerDir);

        //             if (wallcheck == 1)
        //             {
        //                 dot = Vector3.Dot((enemyBody.aiManager.playerTarget.position - basePos).normalized,
        //                                     (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);
        //                 dot2 = Vector3.Dot((rayWallHit.transform.position - basePos).normalized,
        //                                     (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);

        //                 if (CheckAngle(dot, minAngle) && CheckAngle(dot2, awayFromWallAngle))
        //                 {
        //                     done = true;
        //                     posSearchType++;
        //                 }
        //             }
        //             else if (wallcheck == 2)
        //             {
        //                 if (Physics.Raycast(gameObject.transform.position, cornerDir.normalized, out rayCornerHit, 6f, LayerMask.GetMask("Wall")))
        //                 {
        //                     dot = Vector3.Dot((rayCornerHit.transform.position - basePos).normalized,
        //                                         (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);
        //                     if (CheckAngle(dot, -0.2f))
        //                     {
        //                         done = true;
        //                         posSearchType++;
        //                     }
        //                 }
        //             }

        //             if (i == 0)
        //             {
        //                 dot = Vector3.Dot((enemyBody.aiManager.playerTarget.position - basePos).normalized,
        //                                     (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);

        //                 if (CheckAngle(dot, minAngle))
        //                 {
        //                     done = true;
        //                     posSearchType++;
        //                 }
        //             }
        //             else
        //             {
        //                 dot = Vector3.Dot((enemyBody.aiManager.playerTarget.position - basePos).normalized,
        //                                     (new Vector3(random.x, basePos.y, random.z) - basePos).normalized);
        //                 if (CheckAngle(dot, minAngle) && CheckAngle(dot, maxAngle))
        //                 {
        //                     done = true;
        //                     posSearchType++;
        //                 }
        //             }

        //             Debug.Log("Not found");
        //             await UniTask.Yield();
        //             c++;
        //         }
        //     }
        //     // movementPoint = new Vector3(random.x, gameObject.transform.position.y, random.z);
        //     // agent.destination = new Vector3(random.x, gameObject.transform.position.y, random.z);
        //     isRunning = false;
        // }

        // int CheckForWalls(ref Vector3 cornerDir)
        // {
        //     int count = 0;
        //     if (Physics.Raycast(basePos, Vector3.right, out rayWallHit, minWallDistance, LayerMask.GetMask("Wall")))
        //     {
        //         count++;
        //         cornerDir += Vector3.right / 2;
        //     }
        //     if (Physics.Raycast(basePos, -Vector3.right, out rayWallHit, minWallDistance, LayerMask.GetMask("Wall")))
        //     {
        //         count++;
        //         cornerDir -= Vector3.right / 2;
        //     }
        //     if (Physics.Raycast(basePos, Vector3.forward, out rayWallHit, minWallDistance, LayerMask.GetMask("Wall")))
        //     {
        //         count++;
        //         cornerDir += Vector3.forward;
        //     }
        //     if (Physics.Raycast(basePos, -Vector3.forward, out rayWallHit, minWallDistance, LayerMask.GetMask("Wall")))
        //     {
        //         count++;
        //         cornerDir -= Vector3.forward;
        //     }
        //     return count;

        //     // return Physics.Raycast(gameObject.transform.position, (random - gameObject.transform.position), out rayHit, minWallDistance, LayerMask.GetMask("Walls"));
        // }

        // bool CheckAngle(float dot, float angle)
        // {
        //     // if (angle >= 0)
        //     // {
        //     if (dot < angle)
        //     {
        //         if (IsOnNavMesh(random, maxDist))
        //         {
        //             Debug.Log("Find");
        //             basePos = new Vector3(random.x, gameObject.transform.position.y, random.z);
        //             points.movePoints.Add(new Vector3(basePos.x, basePos.y + 0.1f, basePos.z));
        //             var gameobj = new GameObject("waypoint");
        //             gameobj.transform.position = basePos;
        //             return true;
        //         }
        //     }
        //     // }
        //     // else
        //     // {
        //     //     if (dot > angle)
        //     //     {
        //     //         if (IsOnNavMesh(random, maxDist))
        //     //         {
        //     //             Debug.Log("Find");
        //     //             basePos = new Vector3(random.x, gameObject.transform.position.y, random.z);
        //     //             points.movePoints.Add(new Vector3(basePos.x, basePos.y + 0.1f, basePos.z));
        //     //             var gameobj = new GameObject("waypoint");
        //     //             gameobj.transform.position = basePos;
        //     //             return true;
        //     //         }
        //     //     }
        //     // }
        //     return false;
        // }

        // bool IsOnNavMesh(Vector3 random, float maxDist)
        // {
        //     NavMeshQueryFilter f = new NavMeshQueryFilter();
        //     f.agentTypeID = 0;
        //     f.areaMask = 0;

        //     return NavMesh.SamplePosition(random, out hit, 0.5f, f);
        // }
        #endregion
    }
}