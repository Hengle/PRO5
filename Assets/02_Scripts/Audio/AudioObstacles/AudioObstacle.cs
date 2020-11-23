using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public abstract class AudioObstacle : MonoBehaviour
{
    //Zuweisung zu einem Event
    public bool m_onSnare;
    public bool m_onKick;
    public bool m_onHighHat;

    protected bool addedToEvent = false;

    //In welchen Intervall ein Obstacle sich aktiviert
    public bool m_intervalBeat;
    public int m_interval = 2;
    public int m_intervalCounter;
    public int m_startInterval;
    protected Boolean m_IntervalInvert = false;

    //Dauer der Aktion
    public float m_actionInDuration = 0.25f;
    public float m_actionOutDuration = 0.25f;

    //Intensität der Emission & Material
    public float m_maxEmissionIntensity = 10;
    public float m_minEmissionIntensity = 0.1f;
    protected Material _material;

    //Tweening Sequence
    Sequence tweenSeq;

    bool test = false;
    
    



    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    //Wenn Intervall-Modusk aktviert ist wird bei jedem Musik-Marker der IntervallCounter hinaufgezählt
    protected void increaseIntervalCounter()
    {

        if (m_intervalBeat)
        {

            if (!test)
            {
                test = true;
            }
            else
            {
                m_intervalCounter++;
            }
        }

    }

    //Wenn der IntervallCounter mit dem vorgebenen Intervall übereinstimmt wird das zugehörige Event ausgeführt
    protected bool checkInterval()
    {
        if (m_intervalBeat)
        {
            return (m_intervalCounter % m_interval == 0);
        }
        else
        {
            return true;
        }
    }

    //Die Objekt-Action die in der Kind-Klasse dann definiert wird.
    //Jedes Obstacle hat eine Aktion die immer auf das zugehörige Event aktiviert wird.
    protected abstract void objectAction();



    //Wird beim start aufegrufen und checkt anhand der Booleans zu welchen Event es subscriben sollte
    public void addActionToEvent()
    {
        addedToEvent = true;

        if (m_onSnare)
        {
            MyEventSystem.instance.Snare += objectAction;
        }

        if (m_onKick)
        {
            MyEventSystem.instance.Kick += objectAction;
        }

        if (m_onHighHat)
        {
            MyEventSystem.instance.HighHat += objectAction;
        }


    }

    //Unsubscribe vom Event 
    protected void removeActionFromEvent()
    {
        addedToEvent = false;

        if (m_onSnare)
        {
            MyEventSystem.instance.Snare -= objectAction;
        }

        if (m_onKick)
        {
            MyEventSystem.instance.Kick -= objectAction;
        }

        if (m_onHighHat)
        {
            MyEventSystem.instance.HighHat -= objectAction;
        }
    }

    //Änderung der Emission wenn die Aktion des Obstacles ausgeführt wird
    protected void emissionChange(int mode = 0)
    {
        
        if (_material.HasProperty("EmissionIntensity"))
        {
            //On and OFF
            if (mode == 0)
            {
                tweenSeq = DOTween.Sequence()
                .Append(_material.DOFloat(m_maxEmissionIntensity, "EmissionIntensity", m_actionInDuration))
                .Append(_material.DOFloat(m_minEmissionIntensity, "EmissionIntensity", m_actionOutDuration))
                .SetEase(Ease.Flash);
            }

            //ON
            else if (mode == 1)
            {
                tweenSeq = DOTween.Sequence()
                .Append(_material.DOFloat(m_maxEmissionIntensity, "EmissionIntensity", m_actionInDuration))
                .SetEase(Ease.Flash);
            }
            //OFF
            else if (mode == 2)
            {
                tweenSeq = DOTween.Sequence()
                .Append(_material.DOFloat(m_minEmissionIntensity, "EmissionIntensity", m_actionOutDuration))
                .SetEase(Ease.Flash);
            }
        }
    }




}
