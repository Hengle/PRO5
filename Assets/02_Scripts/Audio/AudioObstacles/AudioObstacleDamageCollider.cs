using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Needs rework, my idea is that this class should work for all AudioObstacles that need a Collider for Damage
//but the bool solution to determine parent object is dumb
public class AudioObstacleDamageCollider : MonoBehaviour
{
    //handles if collider is active or not
    bool _disabled = false;

    float _dmgOnEnter = 25;
    float _dmgOnStay = 1;
    
    public void Start()
    {
        //This solution with the bools is a bit dumb, needs rework i would say
        
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