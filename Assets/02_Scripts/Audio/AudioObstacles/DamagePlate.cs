using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

//should work
public class DamagePlate : AudioObstacle, IDamageObstacle
{
    //Are used for the state when de plate holds an value -> activate on beat and deactivate on the next beat

    //When active the colliders are enabled and damage can happen
    public bool _plateActive = false;
    

    private AudioObstacleDamageCollider _childCollider;


    void Start()
    {
        //mlc = FindObjectOfType<MusicLayerController>();
        _material = GetComponent<MeshRenderer>().material;
        _emissionColor = _material.GetColor("_EmissiveColor");
        addActionToEvent();
        // _holdValue = true;
        _materials.Add(_material);
        _childCollider = transform.GetComponentInChildren<AudioObstacleDamageCollider>();
        if (_plateActive)
        {
            emissionActive();
            _holdHelper = false;
        }
    }


  

    protected override void emissionActive()
    {
       
        emissionChange(1);
        _plateActive = true;

        transform.GetComponentInChildren<AudioObstacleDamageCollider>().EnableSelf();
    }

    protected override void emissionDeactive()
    {
        emissionChange(2);
        _plateActive = false;
        transform.GetComponentInChildren<AudioObstacleDamageCollider>().DisableSelf();
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
                       emissionActive();
                       _holdHelper = false;
                    }
                    else
                    {
                        emissionDeactive();
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
            if (obj.GetComponent<IHasHealth>() != null)
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
