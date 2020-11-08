using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;

namespace BBUnity.Actions
{
    [Action("CanAttackCheck")]
    [Help("Checks if the internal timer is done")]
    public class CanAttackCheck : GOAction
    {
        [InParam("canAttack")]
        [OutParam("canAttack")]
        public bool canAttack;

        [InParam("timer")]
        [OutParam("timer")]
        public AIUtilities.Timer timer;

        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        float currentTime;

        public override void OnStart()
        {
            if (timer == null)
            {
                timer = new AIUtilities.Timer();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (!canAttack)
            {
                canAttack = timer.TimerDone();
            }

            return TaskStatus.COMPLETED;
        }
    }
}