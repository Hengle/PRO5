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
    public PowerUpCollectEvent onCollect = new PowerUpCollectEvent();
    public PowerUpController puController;


    protected GameObject _player;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(_player.GetComponents<Collider>()[1]))
        {
            onCollect.Invoke(this);
            
            Destroy(gameObject);
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
