using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioMarkerEmission : AudioObstacle
{

    //Are used for the state when de plate holds an value -> activate on beat and deactivate on the next beat
    public bool _holdValue = false;
    public bool _holdHelper;
    private AudioObstacle _audioObstacleImplementation;


    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _emissionColor = _material.GetColor("_EmissiveColor");
        addActionToEvent();
    }

    // Update is called once per frame


    protected override void objectAction()
    {
        increaseIntervalCounter();
        if (checkInterval())
        {
            if (_holdValue)
            {
                if (_holdHelper)
                {
                    emissionChange(1);
                    _holdHelper = false;
                }
                else
                {
                    emissionChange(2);

                }
            }
            else
            {
                emissionChange();
            }
        }
    }
    


}
