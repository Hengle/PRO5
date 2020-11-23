using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : PowerUp
{
    public GameObject enemy1;


    public override void Activate()
    {
        Debug.Log("MovementSpeedBoost power up activated");
        enemy1.GetComponent<Rigidbody>().AddForce(transform.forward * 1.0f);
    }
}
