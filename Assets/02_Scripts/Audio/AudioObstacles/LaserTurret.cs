using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

//should work
public class LaserTurret : AudioObstacle, IDamageObstacle
{
    private float _minLength;
    public float _maxLength;
    GameObject _energyWall;

    private Material _childMaterial;

    //when active the collider is enabled and damage can happen
    private bool _turretActive = true;
    public bool _startAsActive = false;

    //public float _dmgOnEnter = 30;
    //public float _dmgOnStay = 5;

    public bool useNavMeshForBridges = false;

    // Start is called before the first frame update
    void Start()
    {
        //mlc = FindObjectOfType<MusicLayerController>();
        _material = GetComponent<MeshRenderer>().material;
        _emissionColor = _material.GetColor("_EmissiveColor");
        _energyWall = this.gameObject.transform.GetChild(0).gameObject;
        _minLength = _energyWall.transform.localScale.y;
        addActionToEvent();



        _materials.Add(_material);
        foreach (Transform child in transform)
        {
            if (child.tag == "Turret")
            {
                _materials.Add(child.GetComponent<Renderer>().material);
            }
        }

        if (_startAsActive)
        {
            emissionActive();
            _holdHelper = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (useNavMeshForBridges)
        {
            if (transform.GetChild(0).localScale.y == 0)
            {
                transform.GetComponentInChildren<NavMeshObstacle>().enabled = false;
            }
            else
            {
                transform.GetComponentInChildren<NavMeshObstacle>().enabled = true;
            }
        }

    }

    protected override void emissionActive()
    {
       
        emissionChange(1);  
        foreach (Transform child in transform)
        {
            if(child.tag == "Turret")
            {
                Sequence _tweenSeq = DOTween.Sequence()
                .Append(child.DOScaleY(_maxLength, m_actionInDuration))
                .Join(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor", m_actionInDuration))
                .SetEase(Ease.Flash);
            }

        }
    }

    protected override void emissionDeactive()
    {
       
        emissionChange(2);   
        foreach (Transform child in transform)
        {
            if (child.tag == "Turret")
            {
                Sequence _tweenSeq = DOTween.Sequence()
                .Append(child.DOScaleY(_minLength, m_actionOutDuration))
                .Join(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor", m_actionOutDuration))
                .SetEase(Ease.Flash);
            }
        }
    }


    protected override void objectAction()
    {
        increaseIntervalCounter();
        if (checkInterval())
        {
            //emissionChange();
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
                //ShortDurationHelper();
                foreach (Transform child in transform)
                {
                    Sequence _tweenSeq = DOTween.Sequence()
                        .Append(child.DOScaleY(_maxLength, m_actionInDuration))
                        .Join(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor", m_actionInDuration))
                        .Append(child.DOScaleY(_minLength, m_actionOutDuration))
                        .Join(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor", m_actionOutDuration))
                        .SetEase(Ease.Flash);
                }
            }
        }
    }


//Gets called from dmg collider (child of this object)
//The collision doesnt work the error is in "AudioObstacleDamageCollider"

    public void PullTrigger(Collider c, float dmg)
    {
        if (_turretActive)
        {
            Debug.Log("Turret hit");
            GameObject obj = c.gameObject;
            if (obj.GetComponent<IHasHealth>() != null)
            {
                MyEventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), dmg);
            }
        }
    }


    public void ShortDurationHelper()
    {
        StartCoroutine("EnableDamageRoutine");
    }

    IEnumerator EnableDamageRoutine()
    {
        _turretActive = true;
        gameObject.GetComponentInChildren<AudioObstacleDamageCollider>().EnableSelf();
        yield return new WaitForSeconds(m_actionInDuration + m_actionOutDuration);
        _turretActive = false;
        gameObject.GetComponentInChildren<AudioObstacleDamageCollider>().DisableSelf();
    }
}
