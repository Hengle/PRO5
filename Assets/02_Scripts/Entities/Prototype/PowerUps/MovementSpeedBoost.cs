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

    public override void Activate()
    {
        Debug.Log("MovementSpeedBoost power up activated");
        StartCoroutine(ActivateForDuration(duration));
    }


    protected virtual IEnumerator ActivateForDuration(int duration)
    {
        // @Alex
        // _player.GetComponent<MovementController>().setMovement(2);

        yield return new WaitForSeconds(duration * 1000f);

        // @Alex
        // _player.GetComponent<MovementController>().setMovement(1);

    }
}
