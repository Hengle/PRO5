using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stun : EnemyPowerup
{
    public int duration = 500;

    public override void Activate()
    {
        var enemies = FindEnemies(3.0f);
        // Apply knockback force to each enemy
        var enemyActionsList = enemies.Select(e => e.GetComponent<EnemyActions>());
        foreach (EnemyActions enemyActions in enemyActionsList) StartCoroutine(StunDuration(enemyActions));

        Debug.Log(string.Format("StunPowerUp: Stunned {0} enemies", enemies.Count));
    }


    protected IEnumerator StunDuration(EnemyActions enemyActions)
    {
        enemyActions.isStunned = true;
        yield return new WaitForSeconds(duration);
        enemyActions.isStunned = true;
    }
}