using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    [Action("CanAttackCheck")]
    [Help("Checks if the internal timer is done")]
    public class CanAttackCheck : GOAction
    {
        [InParam("timer")]
        [OutParam("timer")]
        public AIUtilities.Timer timer;

        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        [InParam("attack")]
        public CloseCombatAttacks attack;

        [InParam("actions")]
        public EnemyActions actions;
        [InParam("stats")]
        public EnemyStatistics stats;

        public override void OnStart()
        {
            if (timer == null)
            {
                timer = new AIUtilities.Timer(stats.GetStatValue(StatName.AttackRate));
                if (!actions.canAttack)
                    timer.StartTimer();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (!actions.canAttack)
            {
                actions.canAttack = timer.TimerDone();
            }

            return TaskStatus.COMPLETED;
        }
    }
}