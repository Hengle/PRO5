using System;
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

    public GameObject player;
    public Collider playerCollider;
    public PowerUpCollectEvent onCollect = new PowerUpCollectEvent();


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Debug.Log(playerCollider);
        if (other.Equals(playerCollider))
        {
            onCollect.Invoke(this);
            Destroy(this);
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
