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

        [InParam("timer")]
        public AIUtilities.Timer timer;

        [InParam("attack")]
        public CloseCombatAttacks attack;

        [InParam("animator")]
        public Animator animator;

        [InParam("actions")]
        public EnemyActions actions;
        [InParam("stats")]
        public EnemyStatistics stats;

        public override void OnStart()
        {
            if (timer == null)
            {
                timer = new AIUtilities.Timer(stats.GetStatValue(StatName.AttackRate));
                if (!actions.canAttack && !timer.timerStarted)
                    timer.StartTimer(() => SetCanAttack());
            }
            else if (!actions.canAttack && !timer.timerStarted)
            {
                timer.StartTimer(() => SetCanAttack());
            }
            
            if (actions.canAttack)
            {
                StartAttack();
            }
        }
        public override TaskStatus OnUpdate()
        {
            if (actions.isAttacking)
            {
                gameObject.transform.position = animator.rootPosition;
                return TaskStatus.RUNNING;
            }
            else
            {
                timer.StartTimer(() => SetCanAttack());
                return TaskStatus.COMPLETED;
            }

        }

        private void StartAttack()
        {
            attack.Attack();
            timer.setWaitTime(stats.GetStatValue(StatName.AttackRate));
            actions.canAttack = false;
        }

        public void SetCanAttack()
        {
            actions.canAttack = true;
        }
        public override void OnAbort()
        {
            timer.StartTimer(() => SetCanAttack());
            attack.CancelAttack();
        }
    }
}
