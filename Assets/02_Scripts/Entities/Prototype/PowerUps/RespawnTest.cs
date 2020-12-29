using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTest : PowerUp
{

    public override void Activate()
    {
        Debug.Log("Teleport player to target powerup activated");
        MyEventSystem.instance.OnTeleportPlayer(_player.transform);
    }
}
