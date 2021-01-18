using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class BoomBox : AudioObstacle, IDamageObstacle
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

    public float radius = 5;
    public float duration = 5;
    public float force;
    public bool shouldStun;

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
        Debug.Log("enemy stunned");
        if (_turretActive)
        {
            Debug.Log(c.name);
            /*var enemies = stuff();
            // Apply knockback force to each enemy
            var enemyActionsList = enemies.Select(e => e.GetComponent<EnemyActions>());
            foreach (EnemyActions enemyActions in enemyActionsList) StartCoroutine(StunDuration(enemyActions));

            Debug.Log(string.Format("StunPowerUp: Stunned {0} enemies", enemies.Count));*/
          
            GameObject obj = c.gameObject;
            
            if (obj.GetComponent<EnemyBody>())
            {
                Vector3 direction = (obj.transform.position - transform.position).normalized;

                var rigidbody = obj.GetComponent<Rigidbody>();
                var mass = rigidbody.mass;
                var drag = rigidbody.drag;

                var addForce = mass * (Mathf.Pow(drag + 1, 2)) + force;
                obj.GetComponent<Rigidbody>().AddForce(direction * addForce, ForceMode.Impulse);
                if (shouldStun)
                {
                    StartCoroutine(StunDuration(obj.GetComponent<EnemyActions>()));

                }
            }
        }
    }
    

    protected IEnumerator StunDuration(EnemyActions enemyActions)
    {
        enemyActions.isStunned = true;
        Debug.Log("enemy stunned3");
        yield return new WaitForSeconds(duration);
        enemyActions.isStunned = false;
        //Destroy(this.gameObject);
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