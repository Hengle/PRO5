using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using Cysharp.Threading.Tasks;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/AttackAction")]
    [Help("Attacks")]
    public class AvikAttackAction : GOAction
    {
        [InParam("enemyBody")]
        public EnemyBody enemyBody;

        [InParam("canAttack")]
        [OutParam("canAttack")]
        public bool canAttack;

        [InParam("aiManager")]
        public AIManager aiManager;

        [InParam("timer")]
        public AIUtilities.Timer timer;



        public override void OnStart()
        {
            if (canAttack)
            {
                Attack();
            }
        }
        public override TaskStatus OnUpdate()
        {
            if (canAttack)
                return TaskStatus.RUNNING;

            return TaskStatus.COMPLETED;
        }

        async private void Attack()
        {
            timer.setWaitTime(enemyBody.GetStatValue(StatName.AttackRate));
            Debug.Log("Start Wait");
            await UniTask.Delay(3);

            if (canAttack)
                Debug.Log("Does Damage");
            else
                Debug.Log("No Player there");

            int one = GetFirstAttack();
            bool two = GetSecondAttack(one);
            canAttack = false;
            timer.StartTimer();
            //actions.Attack(one, two);
        }

        int GetFirstAttack()
        {
            int r = Random.Range(0, 100);
            if (r <= 50)
                return 1;
            else
                return 2;
        }

        bool GetSecondAttack(int one)
        {
            int r = Random.Range(0, 100);
            return r <= aiManager.comboBias;
        }

        public override void OnAbort()
        {
            canAttack = false;
        }
    }
}
