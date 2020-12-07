using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

//should work
public class LaserTurret : AudioObstacle, IDamageObstacle
{
    private float _minLength;
    public float _maxLength;
    GameObject _energyWall;

    //when active the collider is enabled and damage can happen
    private bool _turretActive = true;

    //public float _dmgOnEnter = 30;
    //public float _dmgOnStay = 5;

    public float _dmgOnEnter { get; set; }

    public float _dmgOnStay { get; set; }

    public bool _holdValue = false;
    public bool _holdHelper;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _energyWall = this.gameObject.transform.GetChild(0).gameObject;
        _minLength = _energyWall.transform.localScale.y;
        addActionToEvent();
        _dmgOnEnter = 30;
        _dmgOnStay = 5;
    }


    // Update is called once per frame
    void Update()
    {
    }

    protected override void objectAction()
    {
        increaseIntervalCounter();
        if (checkInterval())
        {
            emissionChange();
            if (_holdValue)
            {
                if (_holdHelper)
                {
                    foreach (Transform child in transform)
                    {
                        Sequence _tweenSeq = DOTween.Sequence()
                            .Append(child.DOScaleY(_maxLength, m_actionInDuration))
                            .Join(_material.DOFloat(1, Shader.PropertyToID("EmissionIntensity"), m_actionInDuration))
                            .SetEase(Ease.Flash);
                    }

                    _holdHelper = false;
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        Sequence _tweenSeq = DOTween.Sequence()
                            .Append(child.DOScaleY(_minLength, m_actionOutDuration))
                            .Join(_material.DOFloat(0, Shader.PropertyToID("EmissionIntensity"), m_actionOutDuration))
                            .SetEase(Ease.Flash);
                    }

                    _holdHelper = true;
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    Sequence _tweenSeq = DOTween.Sequence()
                        .Append(child.DOScaleY(_maxLength, m_actionInDuration))
                        .Join(_material.DOFloat(1, Shader.PropertyToID("EmissionIntensity"), m_actionInDuration))
                        .Append(child.DOScaleY(_minLength, m_actionOutDuration))
                        .Join(_material.DOFloat(0, Shader.PropertyToID("EmissionIntensity"), m_actionOutDuration))
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
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
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