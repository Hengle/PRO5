﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PowerUp : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpCollectEvent : UnityEvent<PowerUp>
    {
    }

    public string nameText;
    public string descText;

    public Collider playerCollider;
    public PowerUpCollectEvent onCollect = new PowerUpCollectEvent();


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.Equals(playerCollider))
        {
            onCollect.Invoke(this);
        }
    }

    public virtual void Activate()
    {
        throw new NotImplementedException("Activate method not implemented yet!");
    }

    public override string ToString()
    {
        return string.Format("'PowerUp {0}'", descText);
    }
}
