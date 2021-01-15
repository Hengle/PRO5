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

        [InParam("agent")]
        public NavMeshAgent agent;

        [OutParam("Move to")]
        public List<Vector3> movementPoints;
        NavMeshHit hit;
        Vector3 random;

        bool isRunning = false;
        public override void OnStart()
        {
            if (movementPoints == null)
                movementPoints = new List<Vector3>();
        }
        public override TaskStatus OnUpdate()
        {
            if (!isRunning)
                FindDirection();

            return TaskStatus.COMPLETED;
        }

        async UniTask FindDirection()
        {
            isRunning = true;
            float dot = 1;
            random = gameObject.transform.position;
            bool done = false;

            for (int i = 0; i < 3; i++)
                while (!done)
                {
                    float maxDist = Random.Range(8f, 20f);
                    Vector3 unit = Random.insideUnitSphere * maxDist;
                    random = gameObject.transform.position + unit;

                    dot = Vector3.Dot((enemyBody.aiManager.playerTarget.position - gameObject.transform.position).normalized,
                                        (new Vector3(random.x, gameObject.transform.position.y, random.z) - gameObject.transform.position).normalized);

                    if (dot < minAngle)
                    {
                        if (IsOnNavMesh(random, maxDist))
                        {
                            done = true;
                            movementPoints.Add(new Vector3(random.x, gameObject.transform.position.y, random.z));
                        }
                    }

                    await UniTask.Yield();
                }
            // movementPoint = new Vector3(random.x, gameObject.transform.position.y, random.z);
            // agent.destination = new Vector3(random.x, gameObject.transform.position.y, random.z);
            isRunning = false;
        }

        bool IsOnNavMesh(Vector3 random, float maxDist)
        {
            return NavMesh.SamplePosition(random, out hit, 0.5f, NavMesh.AllAreas);
        }
    }
}