using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{

    [Action("EnemyBehaviour/Attack")]
    [Help("Attacks Object")]
    public class AttackBehaviour : GOAction
    {
        [InParam("animator")]
        
        public Animator animator;

        public override void OnStart()
        {
            Debug.Log("Attack by Enemy");
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }

}
