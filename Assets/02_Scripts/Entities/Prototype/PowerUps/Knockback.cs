using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : PowerUp
{
    public int radius = 3;


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
            Powerups.Knockback kb = new Powerups.Knockback();
            kb.force = 100f;
            kb.powerupName = PowerupNames.Knockback;
            kb.Execute(enemy);
        }
    }
}
