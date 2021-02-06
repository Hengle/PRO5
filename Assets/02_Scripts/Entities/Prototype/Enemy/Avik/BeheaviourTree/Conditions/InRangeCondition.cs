using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;
namespace BBUnity.Conditions
{
    [Condition("IsInRange")]
    public class InRangeCondition : GOCondition
    {
        [InParam("agent")]
        public NavMeshAgent agent;

        [InParam("isInRange")]
        public bool isInRange;

        public override bool Check()
        {
            if (isInRange)
            {
                agent.isStopped = true;
                return true;
            }
            else
            {
                agent.isStopped = false;
                return false;
            }
        }
    }
}