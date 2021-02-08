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
    public FMODUnity.StudioEventEmitter soundEffect;
    public PowerupNames powerupName;
    public GameObject _player;


    private void Start()
    {
        if (!_player) _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && FindObjectOfType<PowerUpController>()._currentPowerUp == null)
        {
            MyEventSystem.instance.OnPowerupCollected(this);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public virtual void Activate(PlayerStateMachine player)
    {
        throw new NotImplementedException("Activate method not implemented yet!");
    }

    public override string ToString()
    {
        return string.Format("'PowerUp {0}'", descText);
    }
}
