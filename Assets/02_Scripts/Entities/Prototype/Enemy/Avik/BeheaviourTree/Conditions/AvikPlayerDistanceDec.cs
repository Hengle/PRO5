using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;


namespace BBUnity.Conditions
{
    [Condition("EnemyBehaviour/CheckTargetDistance")]
    [Help("Checks the Distance to the target")]
    public class AvikPlayerDistanceDec : GOCondition
    {
        [InParam("EnemyBody")]
        public EnemyBody enemyBody;

        [InParam("EnemyAttack")]
        public CloseCombatAttacks attack;

        [InParam("EnemyStats")]
        public EnemyStatistics stats;

        public override bool Check()
        {
            return CheckForPlayer();
        }

        public bool CheckForPlayer()
        {
            if (ScriptCollection.GetScript<AIUtilities>().IsInRange(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.Range)))
            {
                return true;
            }
            else
            {
                return attack.isAttacking;
            }
        }
    }
}
