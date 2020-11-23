using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Conditions
{

    [Condition("EnemyBehaviour/AvoidCondition")]
    [Help("Checks for stuff in the way")]
    public class AvoidDecision : GOCondition
    {
        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        [InParam("stats")]
        public EnemyStatistics stats;

        public override bool Check()
        {
            return IsHeadingForCollision();
        }

        bool IsHeadingForCollision()
        {
            Vector3 dir = Vector3.zero;
            if ((gameObject.transform.position - enemyBody.aiManager.playerTarget.position).sqrMagnitude < stats.GetStatValue(StatName.Range) * stats.GetStatValue(StatName.Range))
            {
                dir = enemyBody.aiManager.playerTarget.position - gameObject.transform.position;
                dir = dir.normalized;
                RaycastHit hit;
                if (Physics.SphereCast(gameObject.transform.position, 0.5f, dir, out hit, 3f, enemyBody.aiManager.enemyMask))
                {
                    Debug.DrawRay(gameObject.transform.position, dir, Color.yellow);

                    return true;
                }
            }
            return false;
        }
    }

}
