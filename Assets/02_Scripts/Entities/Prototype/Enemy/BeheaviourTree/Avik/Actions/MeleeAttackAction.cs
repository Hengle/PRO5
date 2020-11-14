using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;


namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/MeleeAttackAction")]
    [Help("Attacks")]
    public class MeleeAttackAction : GOAction
    {
        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        [InParam("aiManager")]
        public AIManager aiManager;

        [InParam("timer")]
        public AIUtilities.Timer timer;

        [InParam("attack")]
        public CloseCombatAttacks attackUtil;
        public override void OnStart()
        {
            if (attackUtil.canAttack)
            {
                StartAttack();
            }
        }
        public override TaskStatus OnUpdate()
        {
            if (attackUtil.isAttacking)
            {
                return TaskStatus.RUNNING;
            }
            else
            {
                timer.StartTimer();
                return TaskStatus.COMPLETED;
            }

        }

        private void StartAttack()
        {
            attackUtil.Attack();
            timer.setWaitTime(enemyBody.statistics.GetStatValue(StatName.AttackRate));
            attackUtil.canAttack = false;
        }

        public override void OnAbort()
        {
            timer.StartTimer();
            attackUtil.CancelAttack();
        }
    }
}
