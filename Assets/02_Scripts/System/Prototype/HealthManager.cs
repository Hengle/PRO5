using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private void Start()
    {
        MyEventSystem.instance.Attack += HurtEntity;
    }
    
    private void OnDisable()
    {
        MyEventSystem.instance.Attack -= HurtEntity;
    }
    
    public void HurtEntity(IHasHealth entity, float baseDmg)
    {
        entity.TakeDamage(baseDmg);
    }
}
