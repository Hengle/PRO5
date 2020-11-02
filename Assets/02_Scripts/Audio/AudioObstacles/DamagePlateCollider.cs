using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlateCollider : MonoBehaviour
{
    bool disabled = false;

    float dmgOnEnter;
    float dmgOnStay;

    public void Start()
    {
        dmgOnEnter = gameObject.GetComponentInParent<DamagePlate>().dmgOnEnter;
        dmgOnStay = gameObject.GetComponentInParent<DamagePlate>().dmgOnStay;

    }
    void OnTriggerEnter(Collider c)
    {
       
        gameObject.GetComponentInParent<DamagePlate>().PullTrigger(c, dmgOnEnter);
    }

    private void OnTriggerStay(Collider c)
    {
        gameObject.GetComponentInParent<DamagePlate>().PullTrigger(c, dmgOnStay);
    }

    public void DisableSelf()
    {
        GetComponent<BoxCollider>().enabled = false;
        disabled = true;
    }

    public void EnableSelf()
    {
        GetComponent<BoxCollider>().enabled = true;
        disabled = false;
    }



}
