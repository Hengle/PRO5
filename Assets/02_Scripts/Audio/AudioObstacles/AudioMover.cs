using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum backandforth
{
    normal,
    steps
}

public enum rotate
{
    first,
    none
}

//Needs rework
//works only in interval mode and not in normal mode
public class AudioMover : AudioObstacle
{
    //Move with Axis
    public float _moveX = 0;
    public float _moveY = 0;
    public float _moveZ = 0;
    float _posX;

    //Ranges of Axis Movment
    private float _moveXMinBorder;
    private float _moveXMaxBorder;

    private float _moveYMinBorder;
    private float _moveYMaxBorder;

    private float _moveZMinBorder;
    private float _moveZMaxBorder;

    //If the object should move back and forth
    public int rotation = -45;
    private int forthCount = 0;
    private int backCount = 3;
    public int backMax = 3;
    public int forthMax = 3;

    public rotate rot;
    public backandforth backandforth;
    private bool count;
    private Material _symbolMat;

    // Calculating border ranges
    void Start()
    {
        //mlc = FindObjectOfType<MusicLayerController>();
        addActionToEvent();

        _moveXMinBorder = transform.localPosition.x - _moveX;
        _moveXMaxBorder = transform.localPosition.x + _moveX;

        _moveYMinBorder = transform.localPosition.y;
        _moveYMaxBorder = transform.localPosition.y + _moveY;

        _moveZMinBorder = transform.localPosition.z;
        _moveZMaxBorder = transform.localPosition.z + _moveZ;

        _material = GetComponent<MeshRenderer>().material;
        _emissionColor = _material.GetColor("_EmissiveColor");
        foreach (Transform child in transform)
        {
            if (child.tag == "Mover")
            {
                _symbolMat = child.GetComponent<Renderer>().material;
            }
        }
    }

    
    //Movement Code that works not right
    //Doesnt work in Normal Mode 
    protected override void objectAction()
    {
        increaseIntervalCounter();

        Sequence _tweenSeq = DOTween.Sequence()
        .Join(_symbolMat.DOVector(_emissionColor * m_maxEmissionIntensity, "_EmissiveColor", m_actionInDuration))
        .Append(_symbolMat.DOVector(_emissionColor * m_minEmissionIntensity, "_EmissiveColor", m_actionOutDuration))
        .SetEase(Ease.Flash);

        switch (rot)
        {
            case rotate.first:
                AudioRotate();
                activateMoveMethod();
                break;
            case rotate.none:
                activateMoveMethod();
                break;
        }
    }

    void AudioRotate()
    {
        if (_intervalBeat && checkInterval())
        {
            gameObject.transform.DORotate(Vector3.up * rotation,m_actionInDuration,RotateMode.LocalAxisAdd);
        }
    }

    void activateMoveMethod()
    {
        if (_moveX != 0)
        {
            Move('x', _moveXMaxBorder, _moveXMinBorder, m_actionInDuration);
        }

        if (_moveZ != 0)
        {
            Move('z', _moveZMaxBorder, _moveZMinBorder, m_actionInDuration);
        }

        if (_moveY != 0)
        {
            Move('y', _moveYMaxBorder, _moveYMinBorder, m_actionInDuration);
        }
    }

    void Move(char direction, float forthDistance, float backDistance, float duration)
    {
        if (_intervalBeat)
        {
            switch (backandforth)
            {
                case backandforth.normal:
                    if (checkInterval())
                    {
                        makeMove(direction, forthDistance, duration);
                    }

                    if (!checkInterval())
                    {
                        makeMove(direction, backDistance, duration);
                    }

                    break;

                case backandforth.steps:
                    if (checkInterval())
                    {
                        switch (count)
                        {
                            case true:
                                makeMove(direction, backDistance, duration);
                                backCount++;
                                if (backCount == backMax)
                                {
                                    count = false;
                                    backCount = 0;
                                }

                                break;

                            case false:

                                makeMove(direction, forthDistance, duration);
                                forthCount++;
                                if (forthCount == forthMax)
                                {
                                    count = true;
                                    forthCount = 0;
                                }

                                break;
                        }
                    }

                    break;
            }
        }
    }

    void makeMove(char dir, float dis, float dur)
    {
        switch (dir)
        {
            case 'x':
                transform.DOLocalMoveX(dis, dur);
                break;
            case 'y':
                transform.DOLocalMoveY(dis, dur);
                break;
            case 'z':
                transform.DOLocalMoveZ(dis, dur);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _moveXMinBorder = transform.localPosition.x - _moveX;
        _moveXMaxBorder = transform.localPosition.x + _moveX;

        _moveYMinBorder = transform.localPosition.y - _moveY;
        _moveYMaxBorder = transform.localPosition.y + _moveY;

        _moveZMinBorder = transform.localPosition.z - _moveZ;
        _moveZMaxBorder = transform.localPosition.z + _moveZ;
    }

    protected override void emissionActive()
    {

        
    }

    protected override void emissionDeactive()
    {
    }
}