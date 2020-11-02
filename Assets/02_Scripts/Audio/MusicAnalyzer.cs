﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public abstract class MusicAnalyzer : MonoBehaviour
{
    public bool m_onSnare;
    public bool m_onKick;
    public bool m_onHighHat;

    protected bool addedToEvent = false;


    public bool m_onSkillActive;

    public bool m_intervalBeat;
    public int m_interval = 2;
    public int m_intervalCounter;
    public int m_startInterval;
    protected Boolean m_IntervalInvert = false;

    public float m_actionInDuration = 0.25f;
    public float m_actionOutDuration = 0.25f;


    bool test = false;
    Sequence tweenSeq;
    private Material _material;



    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

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

    protected abstract void objectAction();



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

    protected void emissionChange(Material material, int mode = 0)
    {

        if (material.HasProperty("EmissionIntensity"))
        {
            //On and OFF
            if (mode == 0)
            {
                tweenSeq = DOTween.Sequence()
                .Append(material.DOFloat(1, Shader.PropertyToID("EmissionIntensity"), m_actionInDuration))
                .Append(material.DOFloat(0, Shader.PropertyToID("EmissionIntensity"), m_actionOutDuration))
                .SetEase(Ease.Flash);
            }

            //ON
            else if (mode == 1)
            {
                tweenSeq = DOTween.Sequence()
                .Append(material.DOFloat(1, Shader.PropertyToID("EmissionIntensity"), m_actionInDuration))
                .SetEase(Ease.Flash);
            }
            //OFF
            else if (mode == 2)
            {
                tweenSeq = DOTween.Sequence()
                .Append(material.DOFloat(0, Shader.PropertyToID("EmissionIntensity"), m_actionOutDuration))
                .SetEase(Ease.Flash);
            }
        }




    }


}
