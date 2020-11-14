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
        public CloseCombatAttacks attackUtil;

        public override void OnStart()
        {
            if (timer == null)
            {
                timer = new AIUtilities.Timer(enemyBody.statistics.GetStatValue(StatName.AttackRate));
                if (!attackUtil.canAttack)
                    timer.StartTimer();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (!attackUtil.canAttack)
            {
                attackUtil.canAttack = timer.TimerDone();
            }

            return TaskStatus.COMPLETED;
        }
    }
}