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
        public override bool Check()
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 1f,0), Vector3.down, 3f, LayerMask.GetMask("Floor")))
                return false;
            else
                return true;
        }
    }
}