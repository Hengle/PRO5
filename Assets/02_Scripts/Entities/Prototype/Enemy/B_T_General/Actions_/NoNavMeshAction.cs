using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/NoNavMeshAction")]
    public class NoNavMeshAction : GOAction
    {
        [InParam("agent")]
        public NavMeshAgent agent;
        public override void OnStart()
        {
            // agent.enabled = false;
            // ScriptCollection.GetScript<AIUtilities>().DestroyObject(gameObject, 4f);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}