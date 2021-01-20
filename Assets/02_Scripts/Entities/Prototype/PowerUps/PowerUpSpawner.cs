using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerUpSpawner : MonoBehaviour
{
    #region Editor Attributes

    public GameObject player;

    public bool spawnShield;
    public bool spawnKnockback;
    public bool spawnStun;

    #endregion Editor Attributes

    #region Properties and Fields

    public GameObject powerupShield;
    public GameObject powerupKnockback;
    public GameObject powerupStun;

    

    #endregion Properties and Fields

    #region Methods


    public void Spawn()
    {

    }


    public IEnumerator SpawnDelayed()
    {
        yield return null;
    }


    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += player.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    #endregion Methods


    #region Unity Events

    private void Start()
    {

    }

    #endregion Unity Events
}
