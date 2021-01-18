using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            MyEventSystem.instance.OnTeleportPlayer(GameObject.FindObjectOfType<RespawnTest>().transform);
        else if (other.GetComponent<EnemyBody>())
        {
            other.GetComponent<EnemyStatistics>().OnDeath();
        }
    }
}
