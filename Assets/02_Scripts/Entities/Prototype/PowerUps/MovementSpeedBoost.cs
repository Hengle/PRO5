using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementSpeedBoost : PowerUp
{

    public int duration = 200;


    public void Start()
    {
        descText = "Movement Speed Boost";
    }

    public override void Activate(PlayerStateMachine player)
    {
        Debug.Log("MovementSpeedBoost power up activated");
        StartCoroutine(ActivateForDuration(duration, player));
    }


    protected virtual IEnumerator ActivateForDuration(int duration, PlayerStateMachine player)
    {
        // @Alex
        // _player.GetComponent<MovementController>().setMovement(2);

        yield return new WaitForSeconds(duration * 1000f);

        // @Alex
        // _player.GetComponent<MovementController>().setMovement(1);
        Destroy(gameObject);
    }
}
