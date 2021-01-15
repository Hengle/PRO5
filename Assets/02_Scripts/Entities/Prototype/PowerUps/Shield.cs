using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    public int duration = 200;
    public int shieldValue = 20;


    public PlayerStatistics playsterstatistics => _player.GetComponent<PlayerStatistics>();

    public void Start()
    {
        descText = "Shield";
    }

    public override void Activate(PlayerStateMachine player)
    {
        Debug.Log("Shield power up activated");
        StartCoroutine(ActivateForDuration(duration, player));
    }


    protected virtual IEnumerator ActivateForDuration(int duration, PlayerStateMachine player)
    {
        // @Alex
        // _player.ActivateShield(shieldValue)
        playsterstatistics.shieldValue.Value = shieldValue;

        yield return new WaitForSeconds(duration * 1000f);

        // @Alex
        // _player.DecativateShield(shieldValue);
        playsterstatistics.shieldValue.Value = 0;

    }
}
