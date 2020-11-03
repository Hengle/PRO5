using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class AudioMover : AudioObstacle
{
    public float _moveX = 0;
    public float _moveY = 0;
    public float _moveZ = 0;
    float _posX;

    private float _moveXMinBorder;
    private float _moveXMaxBorder;

    private float _moveYMinBorder;
    private float _moveYMaxBorder;

    private float _moveZMinBorder;
    private float _moveZMaxBorder;

    public bool _backAndForth = true;

    float H, S, V;

    float x;

    //public bool _activateComponent = false;



    // Start is called before the first frame update
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

    protected override void objectAction()
    {
        increaseIntervalCounter();
        // moveSimple();

        if (_moveX != 0)
        {
            Debug.Log("HEY");
            if (m_intervalBeat && checkInterval())
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
            if (m_intervalBeat && m_intervalCounter % 2 == 0)
            {

                transform.DOLocalMoveZ(_moveZMaxBorder, m_actionInDuration);
            }
            else if (_backAndForth && (m_intervalCounter % 2 != 0))
            {

                transform.DOLocalMoveZ(_moveZMinBorder, m_actionInDuration);
            }
        }
        if (_moveY != 0)
        {
            if (m_intervalBeat && m_intervalCounter % 2 == 0)
            {
               
                transform.DOLocalMoveY(_moveYMaxBorder, m_actionInDuration);

            }
            else if (_backAndForth && (m_intervalCounter % 2 != 0))
            {

                transform.DOLocalMoveY(_moveYMinBorder, m_actionInDuration);
            }
        }



    }

    void moveSimple()
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

            m_intervalCounter++;
        }
    }




    private void OnEnable()
    {
        // SceneManager.sceneLoaded += addActionToEvent;
        // SceneManager.sceneLoaded += activateComponent;
    }

}


