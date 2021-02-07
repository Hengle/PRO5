using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformParent : MonoBehaviour
{
    PlayerStateMachine player;
    Vector3 oldPos;
    private void Start()
    {
        oldPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            player = other.GetComponent<PlayerStateMachine>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            player = null;
    }

    private void LateUpdate()
    {
        var vel = transform.position - oldPos;
        if (player != null)
            player.transform.position += vel;
        oldPos = transform.position;
    }
}
