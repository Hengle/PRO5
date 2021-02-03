using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Knockback : EnemyPowerup
{
    public float force = 10.0f;
    public float radius = 4f;
    public override void Activate(PlayerStateMachine player)
    {
        
        var enemies = FindEnemies(radius, powerupName, player);
        player.GetComponent<EffectManager>().PlayParticleEffect("knockBack");
        // Apply knockback force to each enemy
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyActions>().FindPowerup(powerupName))
            {
                Vector3 direction = (enemy.transform.position - player.transform.position).normalized;

                var rigidbody = enemy.GetComponent<Rigidbody>();
                var mass = rigidbody.mass;
                var drag = rigidbody.drag;

                var addForce = mass * (Mathf.Pow(drag + 1, 2)) + force;
                StartCoroutine(StunDuration(enemy.GetComponent<EnemyActions>()));
                enemy.GetComponent<Rigidbody>().AddForce(direction * addForce, ForceMode.Impulse);
            }
        }

        Debug.Log(string.Format("KnockbackPowerUp: Kocked back {0} enemies", enemies.Count));
    }

    protected IEnumerator StunDuration(EnemyActions enemyActions)
    {
        enemyActions.isStunned = true;
        yield return new WaitForSeconds(1f);
        enemyActions.isStunned = false;
        Destroy(gameObject);
    }
}
