using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    public int duration = 200;
    public int shieldValue = 20;


    public void Start()
    {
        descText = "Shield";
        onCollect.Invoke(this);
    }

    public override void Activate()
    {
        Debug.Log("Shield power up activated");
        StartCoroutine(ActivateForDuration(duration));
    }


    protected virtual IEnumerator ActivateForDuration(int duration)
    {
        // @Alex
        // _player.ActivateShield(shieldValue)

        yield return new WaitForSeconds(duration * 1000f);

        // @Alex
        // _player.DecativateShield(shieldValue);

    }
}
