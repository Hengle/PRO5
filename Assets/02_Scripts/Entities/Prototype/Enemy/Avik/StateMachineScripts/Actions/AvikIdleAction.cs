using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AvikIdleAction : Action
{
    public override void Execute(StateMachineController controller){
        
        controller.agent.isStopped = false;
        // controller.actions.Attack(-1);
    }
}
