using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvikStunnedDecision : Decision
{
    public override bool Execute(StateMachineController controller)
    {
        if (!controller.stunned)
            return true;
        else
            return false;

    }
}
