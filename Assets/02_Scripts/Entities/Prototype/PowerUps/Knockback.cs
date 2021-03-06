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
        player.GetComponent<EffectManager>().PlayParticleEffect("knockback");
        player.GetComponent<EffectManager>().StopSoundEffect("knockback");

        // Apply knockback force to each enemy
        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.GetComponent<EnemyActions>().FindPowerup(powerupName))
            {
                Vector3 direction = (enemy.transform.position - player.transform.position).normalized;

                var rigidbody = enemy.GetComponent<Rigidbody>();
                var mass = rigidbody.mass;
                var drag = rigidbody.drag;
                var addForce = mass * (Mathf.Pow(drag + 1, 2)) + force;

                enemy.gameObject.GetComponent<EnemyActions>().Stun(1f);
                enemy.gameObject.GetComponent<Rigidbody>().AddForce(direction * addForce, ForceMode.Impulse);

                Destroy(this.gameObject);
            }
        }

        // Debug.Log(string.Format("KnockbackPowerUp: Kocked back {0} enemies", enemies.Count));
    }

    // public IEnumerator StunDuration(EnemyActions enemyActions, Vector3 direction, float addForce)
    // {
    //     enemyActions.Stun();
    //     enemyActions.gameObject.GetComponent<Rigidbody>().AddForce(direction * addForce, ForceMode.Impulse);
    //     yield return new WaitForSeconds(1f);
    //     enemyActions.UnStun();

    // }
}
