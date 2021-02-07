using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Conditions
{
    [Condition("EnemyBehaviour/IsOnNavMesh")]
    public class IsOnNavMeshCheck : GOCondition
    {
        [InParam("agent")]
        public NavMeshAgent agent;

        float waitTime = 0.5f;
        float currentTime;
        public override bool Check()
        {
            if (agent.isOnNavMesh && Physics.Raycast(gameObject.transform.position + new Vector3(0, 0, 0.5f), Vector3.down, 5f, LayerMask.GetMask("Floor"))
                && Physics.Raycast(gameObject.transform.position + new Vector3(0, 0, -0.5f), Vector3.down, 5f, LayerMask.GetMask("Floor")))
            {
                currentTime = 0;
                return false;
            }
            else
            {
                // if (currentTime < waitTime)
                // {
                //     currentTime += Time.deltaTime;
                //     return false;
                // }
                // else
                // {
                currentTime = 0;
                return true;
                // }
            }
        }
    }
}