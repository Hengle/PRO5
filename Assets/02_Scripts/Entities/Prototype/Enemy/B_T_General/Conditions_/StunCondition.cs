using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine.AI;

namespace BBUnity.Conditions
{
    [Condition("EnemyBehaviour/StunCondition")]
    public class StunCondition : GOCondition
    {
        [InParam("EnemyActions")]
        public EnemyActions actions;

        public override bool Check()
        {
            return actions.isStunned;
        }
    }
}