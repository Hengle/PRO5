using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Needs rework, my idea is that this class should work for all AudioObstacles that need a Collider for Damage
//but the bool solution to determine parent object is dumb
public class AudioObstacleDamageCollider : MonoBehaviour
{
    //handles if collider is active or not
    bool _disabled = false;

    public float _dmgOnEnter;
    public float _dmgOnStay;

    public void Start()
    {
        //This solution with the bools is a bit dumb, needs rework i would say
        _dmgOnEnter = GetComponentInParent<AudioObstacle>().dmgOnEnter;
        _dmgOnStay = GetComponentInParent<AudioObstacle>().dmgOnStay;

    }

    //when triggered, the method of the parent PullTrigger gets called that handels the damage
    void OnTriggerEnter(Collider c)
    {
        gameObject.GetComponentInParent<IDamageObstacle>().PullTrigger(c, _dmgOnEnter);
    }

    private void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<IDamageObstacle>().PullTrigger(c, _dmgOnStay);
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