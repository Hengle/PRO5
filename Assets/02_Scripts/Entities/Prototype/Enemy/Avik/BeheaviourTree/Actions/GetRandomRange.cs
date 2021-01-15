using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/GetRandomRange")]
    public class GetRandomRange : GOAction
    {
        [OutParam("randomRange")]
        public float randomRange;

        [InParam("MinRange", typeof(float))]
        public float minRange = 0.0f;

        [InParam("maxRange", typeof(float))]
        public float maxRange = 0.0f;

        [InParam("Stats")]
        public EnemyStatistics stats;

        [InParam("Attack")]
        public CloseCombatAttacks attack;

        public override TaskStatus OnUpdate()
        {
            // if (getRange)
            randomRange = GetRange();
            return TaskStatus.COMPLETED;
        }

        float GetRange()
        {
            // Debug.Log(minRange.ToString() +" and " + maxRange.ToString());
            return Random.Range(minRange, maxRange);
        }
    }
}
