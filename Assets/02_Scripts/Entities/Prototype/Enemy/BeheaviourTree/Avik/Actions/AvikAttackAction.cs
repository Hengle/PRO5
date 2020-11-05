using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;

namespace BBUnity.Actions
{
    [Action("EnemyBehaviour/AttackAction")]
    [Help("Attacks")]
    public class AvikAttackAction : GOAction
    {
        [InParam("canAttack")]
        public bool canAttack = false;

        [InParam("actions")]
        public IEnemyActions actions;

        [InParam("aiManager")]
        public AIManager aiManager;
        public override void OnStart()
        {
            if (canAttack)
            {
                Attack();
                Debug.Log("Enemy Attack");
            }
        }
        
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }

        private void Attack()
        {
            int one = GetFirstAttack();
            bool two = GetSecondAttack(one);
            actions.Attack(one, two);
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
    }
}
