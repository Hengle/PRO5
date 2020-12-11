using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPowerup : PowerUp
{

    protected List<GameObject> FindEnemies(float radius)
    {
        List<GameObject> enemies = new List<GameObject>();
        RaycastHit[] hits = Physics.SphereCastAll(_player.transform.position, radius, _player.transform.forward, radius + 1f);
        // Get all gameobject with EnemyBody attached
        return hits
            .Where(h => h.transform.GetComponent<EnemyBody>())
            .Select(h => h.transform.gameObject)
            .ToList();
    }


}
