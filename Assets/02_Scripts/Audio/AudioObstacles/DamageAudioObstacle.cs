using System;
using DG.Tweening;
using UnityEngine;


public abstract class DamageAudioObstacle : AudioObstacle
{
     //Subscribe to Event
    // public bool m_onSnare;
    // public bool m_onKick;
    // public bool m_onHiHat;

    // public bool _useThisEmission;

    // protected bool addedToEvent = false;

    // //Interval in which the object calls the method "objectAction"
    // public bool _intervalBeat;
    // public float m_interval = 2;
    // public int m_intervalCounter;

    // protected Boolean m_IntervalInvert = false;

    // //Duration of Action
    // public float m_actionInDuration = 0.25f;
    // public float m_actionOutDuration = 0.25f;

    // //Intensity of Material Emission
    // public float m_maxEmissionIntensity = 10;
    // public float m_minEmissionIntensity = 0.1f;
    // protected Material _material;
    // protected Color _emissionColor;
    protected bool alreadyDone = false;

    //Tweening Sequence
    Sequence tweenSeq;

    bool test = false;
    public bool _holdOnMusic;

    [SerializeField] public MusicLayerController mlc;
    public musicEvent a;
    private bool currentActiveLayer;
    // public bool _holdValue = false;
    // protected bool _holdHelper;
    


    void Start()
    {
    }

    void Update()
    {
        HoldMusicEffect();
    }

    void HoldMusicEffect()
    {
        if (_holdOnMusic)
        {
            if (checkActiveMusicLayer() && !alreadyDone)
            {
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
        switch (a)
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
    }

    protected override void emissionActive()
    {
        emissionChange(1);
    }


    protected override void emissionDeactive()
    {
        emissionChange(2); 
    }


    // //Wenn Intervall-Modusk aktviert ist wird bei jedem Musik-Marker der IntervallCounter hinaufgezählt
    // protected void increaseIntervalCounter()
    // {
    //     if (_intervalBeat)
    //     {
    //         m_intervalCounter++;
    //     }
    // }

    //Wenn der IntervallCounter mit dem vorgebenen Intervall übereinstimmt wird das zugehörige Event ausgeführt
    // protected bool checkInterval()
    // {
    //     if (_intervalBeat)
    //     {
    //         return (m_intervalCounter % m_interval == 0);
    //     }
    //     else
    //     {
    //         return true;
    //     }
    // }

    //Die Objekt-Action die in der Kind-Klasse dann definiert wird.
    //Jedes Obstacle hat eine Aktion die immer auf das zugehörige Event aktiviert wird.
    //protected abstract void objectAction();


    //Wird beim start aufegrufen und checkt anhand der Booleans zu welchen Event es subscriben sollte
    // public void addActionToEvent()
    // {
    //     addedToEvent = true;

    //     if (m_onSnare)
    //     {
    //         MyEventSystem.instance.Snare += objectAction;
    //     }

    //     if (m_onKick)
    //     {
    //         MyEventSystem.instance.Kick += objectAction;
    //     }

    //     if (m_onHiHat)
    //     {
    //         MyEventSystem.instance.HiHat += objectAction;
    //     }

    //     /*switch (_events)
    //     {
    //         case musicEvents.m_onKick:
    //             MyEventSystem.instance.Kick += objectAction;
    //             break;
    //         case musicEvents.m_onSnare:
    //             MyEventSystem.instance.Snare += objectAction;
    //             break;
    //         case musicEvents.m_onHitHat:
    //             MyEventSystem.instance.HiHat += objectAction;
    //             break;
    //         default:
    //             break;
                
    //     }*/
    // }

    // //Unsubscribe vom Event 
    // protected void removeActionFromEvent()
    // {
    //     addedToEvent = false;
    //     /*switch (_events)
    //     {
    //         case musicEvents.m_onKick:
    //             MyEventSystem.instance.Kick -= objectAction;
    //             break;
    //         case musicEvents.m_onSnare:
    //             MyEventSystem.instance.Snare -= objectAction;
    //             break;
    //         case musicEvents.m_onHitHat:
    //             MyEventSystem.instance.HiHat -= objectAction;
    //             break;
    //         default:
    //             break;
                
    //     }*/

    //     if (m_onSnare)
    //     {
    //         MyEventSystem.instance.Snare -= objectAction;
    //     }

    //     if (m_onKick)
    //     {
    //         MyEventSystem.instance.Kick -= objectAction;
    //     }

    //     if (m_onHiHat)
    //     {
    //         MyEventSystem.instance.HiHat -= objectAction;
    //     }
    // }

    // private void OnDisable()
    // {
    //     removeActionFromEvent();
    // }

    // //Änderung der Emission wenn die Aktion des Obstacles ausgeführt wird
    // protected void emissionChange(int mode = 0)
    // {
    //     if (_useThisEmission)
    //     {
    //         //On and OFF
    //         if (mode == 0)
    //         {
    //             tweenSeq = DOTween.Sequence()
    //                 .Append(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor",
    //                     m_actionInDuration))
    //                 .Append(_material.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor",
    //                     m_actionOutDuration))
    //                 .SetEase(Ease.Flash);
    //         }
    //         //ON
    //         else if (mode == 1)
    //         {
    //             tweenSeq = DOTween.Sequence()
    //                 .Append(_material.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor",
    //                     m_actionInDuration))
    //                 .SetEase(Ease.Flash);
    //         }
    //         //OFF
    //         else if (mode == 2)
    //         {
    //             tweenSeq = DOTween.Sequence()
    //                 .Append(_material.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor",
    //                     m_actionOutDuration))
    //                 .SetEase(Ease.Flash);
    //         }
    //     }
    // }
}
