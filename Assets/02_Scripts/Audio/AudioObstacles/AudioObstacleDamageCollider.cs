using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Needs rework, my idea is that this class should work for all AudioObstacles that need a Collider for Damage
//but the bool solution to determine parent object is dumb
public class AudioObstacleDamageCollider : MonoBehaviour
{
    //handles if collider is active or not
    bool _disabled = false;

    float _dmgOnEnter;
    float _dmgOnStay;

    //To classify the collider
    public bool _turretCollider;
    public bool _dmgPlateCollider;
    public void Start()
    {

        //This solution with the bools is a bit dumb, needs rework i would say
        if (_dmgPlateCollider)
        {
            _dmgOnEnter = gameObject.GetComponentInParent<DamagePlate>()._dmgOnEnter;
            _dmgOnStay = gameObject.GetComponentInParent<DamagePlate>()._dmgOnstay;
        }
        else if (_turretCollider)
        {
            _dmgOnEnter = gameObject.GetComponentInParent<LaserTurret>()._dmgOnEnter;
            _dmgOnStay = gameObject.GetComponentInParent<LaserTurret>()._dmgOnStay;
        }


    }

    //when triggered, the method of the parent PullTrigger gets called that handels the damage
    void OnTriggerEnter(Collider c)
    {
        if (_dmgPlateCollider)
        {
            gameObject.GetComponentInParent<DamagePlate>().PullTrigger(c, _dmgOnEnter);
        }
        else if (_turretCollider)
        {
            gameObject.GetComponentInParent<LaserTurret>().PullTrigger(c, _dmgOnEnter);
        }

    }

    private void OnTriggerStay(Collider c)
    {
        if (_dmgPlateCollider)
        {
            gameObject.GetComponentInParent<DamagePlate>().PullTrigger(c, _dmgOnStay);
        }
        else if (_turretCollider)
        {
            gameObject.GetComponentInParent<LaserTurret>().PullTrigger(c, _dmgOnStay);
        }
    }

    public void DisableSelf()
    {
        GetComponent<BoxCollider>().enabled = false;
        _disabled = true;
    }

    public void EnableSelf()
    {
        GetComponent<BoxCollider>().enabled = true;
        _disabled = false;
    }
}
