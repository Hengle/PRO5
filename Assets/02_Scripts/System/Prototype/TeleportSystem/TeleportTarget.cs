using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTarget : MonoBehaviour
{
    private void Awake()
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
        StartCoroutine(Teleport(player));
    }

    IEnumerator Teleport(Transform player)
    {
        yield return new WaitForEndOfFrame();
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
        // yield return new WaitForEndOfFrame();
        player.GetComponent<PlayerStateMachine>().isTeleporting = false;
    }
}
