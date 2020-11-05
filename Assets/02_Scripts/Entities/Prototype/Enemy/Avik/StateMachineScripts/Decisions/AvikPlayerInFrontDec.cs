using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvikPlayerInFrontDec : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        controller.actions.CheckIsAttacking();
        return CheckFront(controller);
    }

    bool CheckFront(StateMachineController controller)
    {
        RaycastHit hit;
        if (!Physics.SphereCast(controller.RayEmitter.position, 1f, controller.transform.forward, out hit, controller.enemyStats.GetStatValue(StatName.Range), LayerMask.GetMask("Player")) & !controller.isAttacking)
        {
            controller.actions.Walk();
            controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            return true;
        }

        controller.actions.StopWalking();
        controller.agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        return false;
    }

}
