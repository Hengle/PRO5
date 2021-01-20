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
    HiHat
}

public abstract class AudioObstacle : MonoBehaviour
{
    //Subscribe to Event
    public bool m_onSnare;
    public bool m_onKick;
    public bool m_onHiHat;

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
    public bool alreadyDone;

    //Tweening Sequence
    Sequence tweenSeq;

    bool test = false;
    public bool _holdOnMusic = true;

    [SerializeField] public MusicLayerController mlc;
    public musicEvent ListeningOnLayer;
    public bool currentActiveLayer;
    public bool _holdValue = false;
    protected bool _holdHelper;
    


    void Start()
    {
    }

    void Update()
    {
        HoldMusicEffect();
    }

    void HoldMusicEffect()
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
                alreadyDone = true;
            }
            else if (!checkActiveMusicLayer() && alreadyDone)
            {
                emissionDeactive();
                alreadyDone = false;
            }
        }
    }

    public bool checkActiveMusicLayer()
    {
        bool tmp = false;
        switch (ListeningOnLayer)
        {
            case musicEvent.Snare:
                tmp = mlc._snareActive;
                break;

            case musicEvent.HiHat:
                tmp = mlc._hiHatActive;
                break;
            
            case musicEvent.Kick:
                tmp = mlc._leadBassActive;
                break;
        }

        return tmp;
    }

    protected virtual void emissionActive()
    {
        emissionChange(1);
    }


    protected virtual void emissionDeactive()
    {
        emissionChange(2); 
    }


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

        if (m_onSnare)
        {
            MyEventSystem.instance.Snare += objectAction;
        }

        if (m_onKick)
        {
            MyEventSystem.instance.Kick += objectAction;
        }

        if (m_onHiHat)
        {
            MyEventSystem.instance.HiHat += objectAction;
        }

        /*switch (_events)
        {
            case musicEvents.m_onKick:
                MyEventSystem.instance.Kick += objectAction;
                break;
            case musicEvents.m_onSnare:
                MyEventSystem.instance.Snare += objectAction;
                break;
            case musicEvents.m_onHitHat:
                MyEventSystem.instance.HiHat += objectAction;
                break;
            default:
                break;
                
        }*/
    }

    //Unsubscribe vom Event 
    protected void removeActionFromEvent()
    {
        addedToEvent = false;
        /*switch (_events)
        {
            case musicEvents.m_onKick:
                MyEventSystem.instance.Kick -= objectAction;
                break;
            case musicEvents.m_onSnare:
                MyEventSystem.instance.Snare -= objectAction;
                break;
            case musicEvents.m_onHitHat:
                MyEventSystem.instance.HiHat -= objectAction;
                break;
            default:
                break;
                
        }*/

        if (m_onSnare)
        {
            MyEventSystem.instance.Snare -= objectAction;
        }

        if (m_onKick)
        {
            MyEventSystem.instance.Kick -= objectAction;
        }

        if (m_onHiHat)
        {
            MyEventSystem.instance.HiHat -= objectAction;
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
                tweenSeq = DOTween.Sequence()
                    .Append(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor",
                        m_actionInDuration))
                    .Append(_material.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor",
                        m_actionOutDuration))
                    .SetEase(Ease.Flash);
            }
            //ON
            else if (mode == 1)
            {
                tweenSeq = DOTween.Sequence()
                    .Append(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor",
                        m_actionInDuration))
                    .SetEase(Ease.Flash);
            }
            //OFF
            else if (mode == 2)
            {
                tweenSeq = DOTween.Sequence()
                    .Append(_material.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor",
                        m_actionOutDuration))
                    .SetEase(Ease.Flash);
            }
        }
    }
}