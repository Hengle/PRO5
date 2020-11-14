using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShentauActions : IEnemyActions
{
    public float countdown;
    public bool canAttack = false;
    public float bulletForce = 20f;
    public Animator animator;
    public GameObject bullet;
    public Transform bulletPoint;
    public Transform laser;

    public override void Stunned()
    {
        throw new System.NotImplementedException();
    }
}
