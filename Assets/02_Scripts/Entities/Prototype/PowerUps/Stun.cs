using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : PowerUp
{
    public int radius;

    public override void Activate()
    {
        Debug.Log("Knockback power up activated");

        RaycastHit[] raycastHits = Physics.SphereCastAll(
            player.transform.position + player.GetComponent<CharacterController>().center,
            radius,
            player.transform.forward,
            radius
        );

        foreach (RaycastHit hit in raycastHits)
        {
            GameObject enemy = hit.collider.gameObject;
            EnemyActions eActions = enemy.GetComponent<EnemyActions>();
            Powerups.Stun stun = new Powerups.Stun();
            stun.powerupName = PowerupNames.Stun;
            stun.Execute(enemy);
        }
    }
}
