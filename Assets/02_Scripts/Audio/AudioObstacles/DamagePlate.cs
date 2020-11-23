using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class DamagePlate : AudioObstacle
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

    //private Material _material;
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



    protected override void objectAction()
    {
        increaseIntervalCounter();

        if (checkInterval())
        {
            if (m_holdValue)
            {
                if (m_holdHelper)
                {
                    emissionChange(1);
                    m_holdHelper = false;
                    m_plateActive = true;

                    //ChildCollider aktivieren
                    transform.GetComponentInChildren<DamagePlateCollider>().EnableSelf();

                }
                else
                {
                    emissionChange(2);
                    m_holdHelper = true;
                }
            }
            else
            {
                emissionChange();
                shortDurationHelper();
            }

        }
    }

    //Wird vom Child-Collider aufgerufen
    public void PullTrigger(Collider c, float dmg)
    {
        
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

