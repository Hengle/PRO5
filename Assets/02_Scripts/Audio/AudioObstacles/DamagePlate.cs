using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

//should work
public class DamagePlate : AudioObstacle, IDamageObstacle
{
    //Are used for the state when de plate holds an value -> activate on beat and deactivate on the next beat
    public bool _holdValue;
    bool _holdHelper = true;

    //When active the colliders are enabled and damage can happen
    private bool _plateActive = false;
    
    public float _dmgOnEnter { get; set; }
    public float _dmgOnStay { get; set; }
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        addActionToEvent();
        _dmgOnEnter = 30;
        _dmgOnStay = 5;
    }
    
    

    void Update()
    {
    }

    //Has two modes
    //HoldMode meanst that the object activates on Beat and deactivates on the next beat, it holdes the value between the beats
    //NormalMode meanst objeact actives on the Beat for a certain duration and deactives immediatle after this duration
    protected override void objectAction()
    {
        increaseIntervalCounter();
        if (checkInterval())
        {
            if (_holdValue)
            {
                if (_holdHelper)
                {
                    emissionChange(1);
                    _holdHelper = false;
                    _plateActive = true;

                    transform.GetComponentInChildren<AudioObstacleDamageCollider>().EnableSelf();
                }
                else
                {
                    emissionChange(2);
                    transform.GetComponentInChildren<AudioObstacleDamageCollider>().DisableSelf();
                    _holdHelper = true;
                }
            }
            else
            {
                emissionChange();
                shortDurationHelper();
            }
        }
    }

    //Gets called from dmg collider (child of this object)
    public void PullTrigger(Collider c, float dmg)
    {  
        if (_plateActive)
        {
            Debug.Log("Damage Plate hit");
            GameObject obj = c.gameObject;
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                MyEventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), dmg);
            }
            
        }    
    }

   

    public void shortDurationHelper()
    {
        StartCoroutine("enableDmgRoutine");
    }

    IEnumerator enableDmgRoutine()
    {    
        _plateActive = true;
        gameObject.GetComponentInChildren<AudioObstacleDamageCollider>().EnableSelf();

        yield return new WaitForSeconds(m_actionInDuration);
        _plateActive = false;
        gameObject.GetComponentInChildren<AudioObstacleDamageCollider>().DisableSelf();
    }
}

