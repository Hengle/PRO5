using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLayerController : MonoBehaviour
{


    public FMODUnity.StudioEventEmitter _musicEvent;
    bool _snareActive;
    bool _hiHatActive;
    bool _leadBassActive;
    bool _atmoActive;

    // Start is called before the first frame update
    void Start()
    {
        _snareActive = false;
        _hiHatActive = false;
        _leadBassActive = false;
        _atmoActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (_snareActive)
            {
                _musicEvent.SetParameter("SnareLayer", 0);
                _snareActive = false;
            }
            else
            {
                _musicEvent.SetParameter("SnareLayer", 1);
                _snareActive = true;
            }      
        }

        if (Input.GetKeyDown("2"))
        {
            if (_hiHatActive)
            {
                _musicEvent.SetParameter("HiHatLayer", 0);
                _hiHatActive = false;
            }
            else
            {
                _musicEvent.SetParameter("HiHatLayer", 1);
                _hiHatActive = true;
            }
        }

        if (Input.GetKeyDown("3"))
        {
            if (_leadBassActive)
            {
                _musicEvent.SetParameter("LeadBassLayer", 0);
                _leadBassActive = false;
            }
            else
            {
                _musicEvent.SetParameter("LeadBassLayer", 1);
                _leadBassActive = true;
            }
        }

        if (Input.GetKeyDown("4"))
        {
            if (_atmoActive)
            {
                _musicEvent.SetParameter("AtmoLayer", 0);
                _atmoActive = false;
            }
            else
            {
                _musicEvent.SetParameter("AtmoLayer", 1);
                _atmoActive = true;
            }
        }
    }



}
