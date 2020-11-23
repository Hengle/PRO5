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
        public CloseCombatAttacks attack;

        [InParam("actions")]
        public EnemyActions actions;
        [InParam("stats")]
        public EnemyStatistics stats;
        public override void OnStart()
        {
            if (actions.canAttack)
            {
                StartAttack();
            }
        }
        public override TaskStatus OnUpdate()
        {
            if (actions.isAttacking)
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
            attack.Attack();
            timer.setWaitTime(stats.GetStatValue(StatName.AttackRate));
            actions.canAttack = false;
        }

        public override void OnAbort()
        {
            timer.StartTimer();
            attack.CancelAttack();
        }
    }
}
