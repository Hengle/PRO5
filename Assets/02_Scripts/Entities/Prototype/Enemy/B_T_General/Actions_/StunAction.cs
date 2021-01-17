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

        [InParam("animator")]
        public Animator animator;

        public override void OnStart()
        {
            //start effects or whatever
            agent.enabled = false;
            animator.SetBool("isStunned", true);

        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.RUNNING;
        }

        public override void OnAbort()
        {
            animator.SetBool("isStunned", false);
            agent.enabled = true;
        }
        public override void OnEnd()
        {
            animator.SetBool("isStunned", false);
            agent.enabled = true;
        }
    }
}