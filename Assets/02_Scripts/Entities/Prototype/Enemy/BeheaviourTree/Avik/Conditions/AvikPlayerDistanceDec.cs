﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;


namespace BBUnity.Conditions
{
    [Condition("EnemyBehaviour/CheckTargetDistance")]
    [Help("Checks the Distance to the target")]
    public class AvikPlayerDistanceDec : GOCondition
    {
        [InParam("enemyBody", typeof(EnemyBody))]
        public EnemyBody enemyBody;
        
        [InParam("agent", typeof(NavMeshAgent))]
        public NavMeshAgent agent;

        [InParam("attack")]
        public CloseCombatAttacks attack;

        [InParam("stats")]
        public EnemyStatistics stats;

        [InParam("actions")]
        public EnemyActions actions;
        public override bool Check()
        {
            return CheckForPlayer();
        }

        public bool CheckForPlayer()
        {
            if ((gameObject.transform.position - enemyBody.aiManager.playerTarget.position).sqrMagnitude < Mathf.Pow(stats.GetStatValue(StatName.Range), 2))
            {
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                return true;
            }
            else
            {
                if (actions.isAttacking)
                {
                    // actions.Walk();
                    agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                    return true;
                }
                else
                {
                    agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                    return false;
                }
            }
        }

        bool CheckInFront()
        {
            Ray ray = new Ray(enemyBody.rayEmitter.position, gameObject.transform.forward);
            return Physics.SphereCast(ray, 0.2f, stats.GetStatValue(StatName.Range), LayerMask.GetMask("Player"));
        }
    }
}
