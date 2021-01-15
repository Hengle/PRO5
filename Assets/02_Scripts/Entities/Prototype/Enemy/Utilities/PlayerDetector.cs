using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public PlayerStatistics player;

    void OnTriggerEnter(Collider other)
    {
        PlayerStatistics p = other.gameObject.GetComponent<PlayerStatistics>();
        if (p != null)
            player = p;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStatistics>())
            player = null;
    }
}
