using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Actions;
[Action("EnemyBehaviour/TurnToPlayer")]
public class TurnToPlayerAction : GOAction
{
    [InParam("EnemyBody")]
    public EnemyBody enemyBody;

    [InParam("Stats")]
    public EnemyStatistics stats;

    [InParam("WaitTime")]
    public float waitTime;
    float currentTime;
    AIUtilities utilities;


    public override void OnStart()
    {
        utilities = ScriptCollection.GetScript<AIUtilities>();
    }

    public override TaskStatus OnUpdate()
    {
        if (enemyBody.playerDetector.player == null)
        {
            utilities.LookAtTarget(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.TurnSpeed));
            return TaskStatus.RUNNING;
        }
        else
        {
            utilities.LookAtTarget(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.TurnSpeed));
            currentTime += Time.deltaTime;
            if (currentTime >= waitTime)
            {
                currentTime -= 0;
                return TaskStatus.COMPLETED;
            }
            return TaskStatus.RUNNING;
        }
    }
}
