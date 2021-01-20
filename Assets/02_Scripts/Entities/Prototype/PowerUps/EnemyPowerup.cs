using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPowerup : PowerUp
{
    protected List<GameObject> FindEnemies(float radius, PowerupNames powerupName, PlayerStateMachine player)
    {
        List<GameObject> enemies = new List<GameObject>();
        Collider[] hits = Physics.OverlapSphere(player.transform.position, radius);
        // Get all gameobject with EnemyBody script attached

        return hits
            .Where(h => h.transform.GetComponent<EnemyActions>())
            .Select(h => h.transform.gameObject)
            .ToList();

    }
}
