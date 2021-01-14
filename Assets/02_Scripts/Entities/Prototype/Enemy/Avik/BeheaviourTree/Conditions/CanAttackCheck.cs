using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using BBUnity.Actions;


[Action("CanAttackCheck")]
public class CanAttackCheck : GOAction
{
    [InParam("WaitTime")]
    public float waitTime;

    [OutParam("waitTimeDone")]
    public bool waitTimeDone;
    float currentTime;

    public override TaskStatus OnUpdate()
    {
        if (!waitTimeDone)
            currentTime += Time.deltaTime;

        if (currentTime >= waitTime)
        {
            waitTimeDone = true;
            currentTime -= waitTime;
        }
        return TaskStatus.COMPLETED;
    }
}
