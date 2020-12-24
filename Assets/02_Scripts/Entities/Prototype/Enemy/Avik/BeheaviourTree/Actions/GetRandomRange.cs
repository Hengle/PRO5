using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    public class GetRandomRange : GOAction
    {
        [OutParam("Range", typeof(float))]
        public float range;

        [InParam("MinRange")]
        public float minRange;

        float maxRange;

        [InParam("Stats")]
        public EnemyStatistics stats;

        [InParam("Attack")]
        public CloseCombatAttacks attack;

        [InParam("GetRange")]
        [OutParam("GetRange")]
        public bool getRange;

        public override void OnStart()
        {
            maxRange = stats.GetStatValue(StatName.Range);
        }

        public override TaskStatus OnUpdate()
        {
            if (getRange)
                range = GetRange();
            return TaskStatus.COMPLETED;
        }

        float GetRange()
        {
            float f = 0;
            float r = Random.Range(-1f, 1f);
            f = maxRange / 2 + r;
            if (f < minRange)
                return minRange;
            if (f > maxRange)
                return maxRange - 0.1f;

            getRange = false;
            return f;
        }
    }
}
