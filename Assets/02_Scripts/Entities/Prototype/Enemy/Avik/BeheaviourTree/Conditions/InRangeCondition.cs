using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;

namespace BBUnity.Conditions
{
    [Condition("IsInRange")]
    public class InRangeCondition : GOCondition
    {
        [InParam("isInRange")]
        public bool isInRange;

        public override bool Check()
        {
            return isInRange;
        }
    }
}