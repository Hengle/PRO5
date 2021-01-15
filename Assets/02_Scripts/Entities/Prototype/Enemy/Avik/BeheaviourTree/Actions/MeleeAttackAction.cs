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

        public AIUtilities.Timer timer;

        [InParam("attack")]
        public CloseCombatAttacks attack;

        [InParam("animator")]
        public Animator animator;

        [InParam("stats")]
        public EnemyStatistics stats;

        [InParam("InitAttack")]
        public bool initAttack;
        
        public override void OnStart()
        {
            if (timer == null)
            {
                timer = new AIUtilities.Timer(stats.GetStatValue(StatName.AttackRate));
                if (!attack.canAttack && !timer.timerStarted)
                    timer.StartTimer(() => SetCanAttack());
            }
            else if (!attack.canAttack && !timer.timerStarted)
            {
                timer.StartTimer(() => SetCanAttack());
            }

            if(attack.canAttack)
            {
                StartAttack();
            }
        }
        public override TaskStatus OnUpdate()
        {
            if (attack.isAttacking)
            {
                // gameObject.transform.position = animator.rootPosition;
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
            attack.canAttack = false;
            initAttack = false;
        }

        public void SetCanAttack()
        {
            attack.canAttack = true;
        }
        public override void OnAbort()
        {
            timer.StartTimer(() => SetCanAttack());
            attack.CancelAttack();
        }
    }
}
