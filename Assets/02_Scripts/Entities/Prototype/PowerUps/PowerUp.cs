using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PowerUp : MonoBehaviour
{

    public string nameText;
    public string descText;


    public GameObject _player;


    private void Start()
    {
        if (!_player) _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(_player.GetComponents<Collider>()[1]))
        {
            MyEventSystem.instance.OnPowerupCollected(this);
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
