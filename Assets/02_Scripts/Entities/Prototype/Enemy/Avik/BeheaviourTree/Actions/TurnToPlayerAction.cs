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

    [InParam("InitAttack")]
    public bool initAttack;
    float currentTime;
    AIUtilities utilities;

    bool wasIn = true;
    public override void OnStart()
    {
        if (enemyBody.playerDetector.player != null)
            wasIn = true;
        else
            wasIn = false;

        utilities = ScriptCollection.GetScript<AIUtilities>();
    }

    public override TaskStatus OnUpdate()
    {
        if (!initAttack)
            if (enemyBody.playerDetector.player == null)
            {
                wasIn = false;
                utilities.LookAtTarget(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.TurnSpeed));
                return TaskStatus.RUNNING;
            }
            else
            {
                utilities.LookAtTarget(enemyBody.aiManager.playerTarget, gameObject.transform, stats.GetStatValue(StatName.TurnSpeed));
                if (!wasIn)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= waitTime)
                    {
                        currentTime = 0;
                        wasIn = true;
                        return TaskStatus.COMPLETED;
                    }
                }
                else
                {
                    return TaskStatus.COMPLETED;
                }
                return TaskStatus.RUNNING;
            }
        return TaskStatus.COMPLETED;
    }
}
