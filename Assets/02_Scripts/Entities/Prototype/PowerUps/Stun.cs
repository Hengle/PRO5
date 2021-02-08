using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stun : EnemyPowerup
{
    public int duration = 5;
    public float radius = 4f;
    public override void Activate(PlayerStateMachine player)
    {
        var enemies = FindEnemies(radius, powerupName, player);
        player.GetComponent<EffectManager>().PlayParticleEffect("stun");
        // Apply knockback force to each enemy
        var enemyActionsList = enemies.Select(e => e.GetComponent<EnemyActions>());
        foreach (EnemyActions enemyActions in enemyActionsList) enemyActions.Stun(duration);
        
        // Debug.Log(string.Format("StunPowerUp: Stunned {0} enemies", enemies.Count));
    }

    // protected IEnumerator StunDuration(EnemyActions enemyActions)
    // {

    //     yield return new WaitForSeconds(duration);
    //     enemyActions.UnStun();
    //     Destroy(this.gameObject);
    // }
}