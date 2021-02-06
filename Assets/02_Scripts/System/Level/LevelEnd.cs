using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerStateMachine>();
        if (player != null)
        {
            player.inputManager.controls.Disable();
            GameManager.instance.NextLevel();
        }
    }
}
