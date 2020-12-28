using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/StunAction")]
    public class StunAction : GOAction
    {
        [InParam("agent")]
        public NavMeshAgent agent;


        public override void OnStart()
        {
            //start effects or whatever
            // agent.enabled = false;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }

        public override void OnAbort()
        {
            // agent.enabled = true;
        }
        public override void OnEnd()
        {
            // agent.enabled = true;
        }
    }
}