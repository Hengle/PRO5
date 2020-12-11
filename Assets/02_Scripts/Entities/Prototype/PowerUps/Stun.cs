using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : EnemyPowerup
{
    public int duration = 500;

    public override void Activate()
    {
        var enemies = FindEnemies(3.0f);
        // Apply knockback force to each enemy
        foreach (GameObject enemy in enemies)
        {
        }


        Debug.Log(string.Format("Stunned {0} enemies", enemies.Count));
    }
}
