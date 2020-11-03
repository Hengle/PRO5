using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LaserTurret : AudioObstacle
{
    public bool lengthChange = false;
    Material _energyWallMaterial;


    private float defaultLength;
    public float lengthOfLaser;

    GameObject _energyWall;
    Color _energyWallColor;
    Color _beaconEmissiveColor;
    float H, S, V;

    private bool m_beaconActive = true;

    public bool m_activateComponent = false;


    Sequence tweenSeq;

    private void OnEnable()
    {

    }

    public float dmgOnEnter = 30;
    public float dmgOnStay = 5;

    // Start is called before the first frame update
    void Start()
    {

        _material = GetComponent<MeshRenderer>().material;
        _energyWall = this.gameObject.transform.GetChild(0).gameObject;


        defaultLength = _energyWall.transform.localScale.y;

        addActionToEvent();

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
            /*
            tweenSeq = DOTween.Sequence()
                 .Append(_material.DOFloat(1, Shader.PropertyToID("EmissionIntensity"), m_actionInDuration))
                 .Append(_material.DOFloat(0, Shader.PropertyToID("EmissionIntensity"), m_actionOutDuration))
                 .SetEase(Ease.Flash);
                 */
            emissionChange();

            // shortDurationHelper();
            foreach (Transform child in transform)
            {
                tweenSeq = DOTween.Sequence()
                .Append(child.DOScaleY(lengthOfLaser, m_actionInDuration))
                .Join(_material.DOFloat(1, Shader.PropertyToID("EmissionIntensity"), m_actionInDuration))
                .Append(child.DOScaleY(defaultLength, m_actionOutDuration))
                .Join(_material.DOFloat(0, Shader.PropertyToID("EmissionIntensity"), m_actionOutDuration))
                .SetEase(Ease.Flash);
            }

        }
    }

    public void PullTrigger(Collider c, float dmg)
    {
        //Debug.Log("BeaconWallTrigger is pulled");
        if (m_beaconActive)
        {
            //Debug.Log("BeaconWall hit");
            GameObject obj = c.gameObject;
            /*
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                MyEventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), dmg);
            }
            */
        }
    }

    public void shortDurationHelper()
    {
        StartCoroutine("enableDmgRoutine");
    }

    IEnumerator enableDmgRoutine()
    {

        m_beaconActive = true;
        //gameObject.GetComponentInChildren<AR_beaconWallCollider>().EnableSelf();

        yield return new WaitForSeconds(m_actionInDuration + m_actionOutDuration);
        m_beaconActive = false;
        //gameObject.GetComponentInChildren<AR_beaconWallCollider>().DisableSelf();
    }





}

