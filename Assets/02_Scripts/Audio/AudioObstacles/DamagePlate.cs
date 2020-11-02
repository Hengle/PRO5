using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class DamagePlate : MusicAnalyzer
{
    private Material m_material;
    private float defaultLength;

    public bool m_holdValue;
    bool m_holdHelper = true;
    Sequence mySequence;

    private bool m_plateActive = false;

    Color m_color;
    float H, S, V;
    float H1, S1, V1;
    float H2, S2, V2;

    private Material _material;
    public bool m_activateComponent = false;
    private bool activationhelper = false;

    public float dmgOnEnter = 30;
    public float dmgOnStay = 5;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        m_startInterval = m_intervalCounter + 1;

        BoxCollider a = GetComponentInChildren<BoxCollider>();

        addActionToEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator activationDelayNumerator()
    {
        yield return new WaitForSeconds(1f);
        // m_activateComponent = true;
    }


    protected override void objectAction()
    {

        increaseIntervalCounter();

        if (checkInterval())
        {
            if (m_holdValue)
            {
                if (m_holdHelper)
                {
                    emissionChange(_material, 1);
                    m_holdHelper = false;
                    /*
                    m_plateActive = true;
                    V = 10;
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionInDuration))
                    .SetEase(Ease.Flash);
                    
                    gameObject.GetComponentInChildren<DamagePlateCollider>().EnableSelf();
                    */
                }
                else
                {
                    emissionChange(_material, 2);
                    m_holdHelper = true;
                    /*
                    V = -10;
                   
                    mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, V, true), "EmissionRedColor", m_actionOutDuration))
                    .SetEase(Ease.Flash);
                    m_plateActive = false;

                    gameObject.GetComponentInChildren<DamagePlateCollider>().DisableSelf();
                    */
                }
            }
            else
            {
                emissionChange(_material);
                /*
                shortDurationHelper();

                mySequence = DOTween.Sequence()
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, 10, true), "EmissionRedColor", m_actionInDuration))
                    .Append(m_material.DOColor(Color.HSVToRGB(H, S, -10, true), "EmissionRedColor", m_actionOutDuration))
                    .SetEase(Ease.Flash);
                    */
            }

        }
    }


    public void PullTrigger(Collider c, float dmg)
    {
        /*
        bool hit = false;
        if (m_plateActive)
        {
            Debug.Log("Damage Plate hit");
            GameObject obj = c.gameObject;
            if (!obj.GetComponent<EnemyBody>() & obj.GetComponent<IHasHealth>() != null)
            {
                MyEventSystem.instance.OnAttack(obj.GetComponent<IHasHealth>(), dmg);
                hit = true;
            }

        }
        */
    }
    private void OnEnable()
    {
        // SceneManager.sceneLoaded += addActionToEvent;
        // SceneManager.sceneLoaded += activateComponent;
    }
    public void shortDurationHelper()
    {
        StartCoroutine("enableDmgRoutine");
    }

    IEnumerator enableDmgRoutine()
    {

        m_plateActive = true;
        gameObject.GetComponentInChildren<DamagePlateCollider>().EnableSelf();

        yield return new WaitForSeconds(m_actionInDuration);
        m_plateActive = false;
        gameObject.GetComponentInChildren<DamagePlateCollider>().DisableSelf();
    }

}

