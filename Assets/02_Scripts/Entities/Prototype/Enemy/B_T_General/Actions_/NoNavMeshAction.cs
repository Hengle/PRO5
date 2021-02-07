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

        bool doOnce = false;
        public override void OnStart()
        {

        }

        public override TaskStatus OnUpdate()
        {
            if (agent.isOnNavMesh && Physics.Raycast(gameObject.transform.position, Vector3.down, 5f, LayerMask.GetMask("Floor")))
            {
                agent.isStopped = false;
                agent.enabled = true;
                return TaskStatus.COMPLETED;
            }
            else
            {
                if (!doOnce)
                {
                    doOnce = true;
                    ScriptCollection.GetScript<AIUtilities>().DestroyObject(gameObject, 4f);
                }
                agent.isStopped = true;
                agent.enabled = false;
                return TaskStatus.RUNNING;
            }
        }
    }
}