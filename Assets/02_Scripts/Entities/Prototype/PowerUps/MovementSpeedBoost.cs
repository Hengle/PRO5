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
        //Thomas fügte diese Zeile hinzu, da ansonsten aus irgendeinem Grund der Spieler nicht gefunden werden kann
        //Obwohl in der parent Class PowerUp das selbe ausgeführt wird
        if (!_player) _player = GameObject.FindGameObjectWithTag("Player");

        descText = "Movement Speed Boost";
    }

    public override void Activate(PlayerStateMachine player)
    {
        Debug.Log("MovementSpeedBoost power up activated");
        player.GetComponent<EffectManager>().PlayParticleEffect("speedBoost");
        StartCoroutine(ActivateForDuration(duration, player));
    }


    protected virtual IEnumerator ActivateForDuration(int duration, PlayerStateMachine player)
    {
        // @Alex
        // _player.GetComponent<MovementController>().setMovement(2);
        pc.speedBuffMult = speedMult;
        pc.currentMoveSpeed = pc.currentMoveSpeed * speedMult;

        yield return new WaitForSeconds(duration);

        Debug.Log("MovementSpeedBoost power up deactivated");
        // @Alex
        // _player.GetComponent<MovementController>().setMovement(1);
        pc.speedBuffMult = 1;
        pc.currentMoveSpeed = pc.standardMoveSpeed;

    }
}
