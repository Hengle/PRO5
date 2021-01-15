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
            agent.isStopped = true;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.RUNNING;
        }

        public override void OnAbort()
        {
            agent.isStopped = false;
        }
        public override void OnEnd()
        {
            agent.isStopped = false;
        }
    }
}