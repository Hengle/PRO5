using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Knockback : EnemyPowerup
{

    public float force = 10.0f;

    public override void Activate()
    {
        var enemies = FindEnemies(3.0f);
        // Apply knockback force to each enemy
        foreach (GameObject enemy in enemies)
        {
            Vector3 direction = (enemy.transform.position - _player.transform.position).normalized;
            enemy.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        }

        Debug.Log(string.Format("KnockbackPowerUp: Kocked back {0} enemies", enemies.Count));
    }
}
