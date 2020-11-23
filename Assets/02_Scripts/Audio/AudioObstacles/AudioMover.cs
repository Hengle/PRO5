using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;



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
    public bool _backAndForth = true;

    // Calculating border ranges
    void Start()
    {
        addActionToEvent();

        _moveXMinBorder = transform.localPosition.x;
        _moveXMaxBorder = transform.localPosition.x + _moveX;

        _moveYMinBorder = transform.localPosition.y;
        _moveYMaxBorder = transform.localPosition.y + _moveY;
        
        _moveZMinBorder = transform.localPosition.z;
        _moveZMaxBorder = transform.localPosition.z + _moveZ;
    }

    //Movement Code that works not right
    //Doesnt work in Normal Mode 
    protected override void objectAction()
    {
        increaseIntervalCounter();

        if (_moveX != 0)
        {
            if (_intervalBeat && checkInterval())
            {
                transform.DOLocalMoveX(_moveXMaxBorder, m_actionInDuration) ;
            }
            if (_backAndForth && !checkInterval())
            {
                transform.DOLocalMoveX(_moveXMinBorder, m_actionInDuration);
            }
        }
        if (_moveZ != 0)
        {
            if (_intervalBeat && checkInterval())
            {
                transform.DOLocalMoveZ(_moveZMaxBorder, m_actionInDuration);
            }
            else if (_backAndForth && !checkInterval())
            {
                transform.DOLocalMoveZ(_moveZMinBorder, m_actionInDuration);
            }
        }
        if (_moveY != 0)
        {
            if (_intervalBeat && checkInterval())
            {              
                transform.DOLocalMoveY(_moveYMaxBorder, m_actionInDuration);

            }
            else if (_backAndForth && !checkInterval())
            {
                transform.DOLocalMoveY(_moveYMinBorder, m_actionInDuration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

}


