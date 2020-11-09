using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementSpeedBoost : PowerUp
{

    public void Start()
    {
        descText = "Movement Speed Boost";
        onCollect.Invoke(this);
    }

    public override void Activate()
    {
        Debug.Log("MovementSpeedBoost power up activated");
    }
}
