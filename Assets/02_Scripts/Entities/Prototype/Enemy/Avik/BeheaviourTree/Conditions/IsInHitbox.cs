using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Conditions;

[Condition("EnemyBehaviour/IsInHitbox")]
public class IsInHitbox : GOCondition
{
    [InParam("enemyBody")]
    public EnemyBody enemyBody;

    [InParam("EnemyAttack")]
    public CloseCombatAttacks attack;


    public override bool Check()
    {
        if (enemyBody.playerDetector.player == null && !attack.isAttacking)
        {
            return true;
            // else
            //     return false;
        }
        else
        {
            return false;
        }

    }
}
