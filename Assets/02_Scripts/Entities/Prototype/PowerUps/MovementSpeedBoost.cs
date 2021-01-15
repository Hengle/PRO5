using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementSpeedBoost : PowerUp
{

    public int duration = 6;
    public float speedMult = 2;
    public PlayerStateMachine pc => _player.GetComponent<PlayerStateMachine>();
    

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
        pc.currentMoveSpeed = pc.currentMoveSpeed * speedMult;

        yield return new WaitForSeconds(duration);

        // @Alex
        // _player.GetComponent<MovementController>().setMovement(1);
        pc.currentMoveSpeed = pc.standardMoveSpeed;

    }
}
