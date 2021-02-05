using System;
using System.Collections;
using System.Collections.Generic;
using BBUnity.Actions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


//Base Class for every AudioObstacle
//Handles Event Subscribing
//Intervall
//Material Emission
public enum musicEvent
{
    Snare,
    Kick,
    HiHat,
    LeadBass,
    Atmo
}

public abstract class AudioObstacle : MonoBehaviour
{
    //Subscribe to Event

    //[SerializeField] public MusicLayerController mlc;
    public musicEvent ListeningOnLayer;

    [HideInInspector]public bool m_onSnare, m_onKick, m_onHiHat, m_onLeadBass, m_onAtmo;
    public bool _useThisEmission;

    protected bool addedToEvent = false;



    //Interval in which the object calls the method "objectAction"
    public bool _intervalBeat;
    public float m_interval = 2;
    public int m_intervalCounter;

    protected Boolean m_IntervalInvert = false;

    //Duration of Action
    public float m_actionInDuration = 0.25f;
    public float m_actionOutDuration = 0.25f;

    //Intensity of Material Emission
    public float m_maxEmissionIntensity = 10;
    public float m_minEmissionIntensity = 0.1f;
    protected Material _material;
    protected Color _emissionColor;
    //public bool alreadyDone;

    //Tweening Sequence
    Sequence tweenSeq;

    bool test = false;
    //public bool _holdOnMusic = true;



    //public bool currentActiveLayer;
    public bool _holdValue = false;
    protected bool _holdHelper;

    public float dmgOnEnter;
    public float dmgOnStay;


    [HideInInspector] public List<Material> _materials;

    private void Awake()
    {
        /*switch (ListeningOnLayer)
        {
            case musicEvent.Snare:
                m_onSnare = true;

                break;
            case musicEvent.HiHat:
                m_onHiHat = true;

                break;
            case musicEvent.Kick:
                m_onKick = true;

                break;
            case musicEvent.LeadBass:
                m_onLeadBass = true;

                break;
            case musicEvent.Atmo:
                m_onAtmo = true;

                break;
        }*/
        if (_holdValue)
        {
            _holdHelper = true;
        }
    }

    void Start()
    {

    }

    void deactivatebolls()
    {
        m_onSnare = false;
        m_onKick = false;
        m_onHiHat = false;
        m_onLeadBass = false;
        m_onAtmo = false;
    }

    void Update()
    {
        //HoldMusicEffect();
    }

    /*void HoldMusicEffect()
    {
        Debug.Log("test1");
        if (_holdOnMusic)
        {
            Debug.Log("test2");
            currentActiveLayer = checkActiveMusicLayer();
            if (checkActiveMusicLayer() && alreadyDone == false)
            {
                Debug.Log("test3");

                emissionActive();
                Debug.Log("test4");
                alreadyDone = true;
            }
            else if (!checkActiveMusicLayer() && alreadyDone)
            {
                emissionDeactive();
                alreadyDone = false;
            }
        }
    }*/

    /*public bool checkActiveMusicLayer()
    {
        //bool tmp = false;
        switch (ListeningOnLayer)
        {
            case musicEvent.Snare:
                currentActiveLayer = mlc._snareActive;
                break;

            case musicEvent.HiHat:
                currentActiveLayer = mlc._hiHatActive;
                break;

            case musicEvent.Kick:
                currentActiveLayer = mlc._leadBassActive;
                break;
        }

        return currentActiveLayer;
    }*/

    protected abstract void emissionActive();


    protected abstract void emissionDeactive();


    //Wenn Intervall-Modusk aktviert ist wird bei jedem Musik-Marker der IntervallCounter hinaufgezählt
    protected void increaseIntervalCounter()
    {
        if (_intervalBeat)
        {
            m_intervalCounter++;
        }
    }

    //Wenn der IntervallCounter mit dem vorgebenen Intervall übereinstimmt wird das zugehörige Event ausgeführt
    protected bool checkInterval()
    {
        if (_intervalBeat)
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

        switch (ListeningOnLayer)
        {
            case musicEvent.Kick:
                MyEventSystem.instance.Kick += objectAction;
                m_onKick = true;
                break;
            case musicEvent.Snare:
                MyEventSystem.instance.Snare += objectAction;
                m_onSnare = true;

                break;
            case musicEvent.HiHat:
                MyEventSystem.instance.HiHat += objectAction;
                m_onHiHat = true;

                break;
            case musicEvent.Atmo:
                MyEventSystem.instance.Atmo += objectAction;
                m_onAtmo = true;

                break;
            case musicEvent.LeadBass:
                MyEventSystem.instance.LeadBass += objectAction;
                m_onLeadBass = true;

                break;
            default:
                break;
        }
    }

    //Unsubscribe vom Event 
    protected void removeActionFromEvent()
    {
        addedToEvent = false;
        switch (ListeningOnLayer)
        {
            case musicEvent.Kick:
                MyEventSystem.instance.Kick -= objectAction;
                m_onKick = true;

                break;
            case musicEvent.Snare:
                MyEventSystem.instance.Snare -= objectAction;
                m_onSnare = true;

                break;
            case musicEvent.HiHat:
                MyEventSystem.instance.HiHat -= objectAction;
                m_onHiHat = true;

                break;
            case musicEvent.Atmo:

                MyEventSystem.instance.Atmo -= objectAction;
                m_onAtmo = true;

                break;
            case musicEvent.LeadBass:
                MyEventSystem.instance.LeadBass -= objectAction;
                m_onLeadBass = true;

                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        removeActionFromEvent();
    }

    //Änderung der Emission wenn die Aktion des Obstacles ausgeführt wird
    protected void emissionChange(int mode = 0)
    {
        if (_useThisEmission)
        {
            //On and OFF
            if (mode == 0)
            {
                foreach (Material _material in _materials)
                {
                    tweenSeq = DOTween.Sequence()
                        .Append(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor",
                            m_actionInDuration))
                        .Append(_material.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor",
                            m_actionOutDuration))
                        .SetEase(Ease.Flash);
                }
            }
            //ON
            else if (mode == 1)
            {
                foreach (Material _material in _materials)
                {
                    tweenSeq = DOTween.Sequence()
                        .Append(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor",
                            m_actionInDuration))
                        .SetEase(Ease.Flash);
                }
            }
            //OFF
            else if (mode == 2)
            {
                foreach (Material _material in _materials)
                {
                    tweenSeq = DOTween.Sequence()
                        .Append(_material.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor",
                            m_actionOutDuration))
                        .SetEase(Ease.Flash);
                }
            }
        }
    }
}