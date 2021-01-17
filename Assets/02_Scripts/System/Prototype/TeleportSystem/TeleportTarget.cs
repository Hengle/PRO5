using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTarget : MonoBehaviour
{
    private void Start()
    {
        MyEventSystem.instance.teleportPlayer += TeleportPlayerToThis;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.teleportPlayer -= TeleportPlayerToThis;
    }


    private void TeleportPlayerToThis(Transform player) 
    {
        player.GetComponent<PlayerStateMachine>().isTeleporting = true;
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
    }
}
