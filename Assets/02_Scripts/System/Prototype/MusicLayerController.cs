using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicLayerController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter _musicEvent;

    [HideInInspector]
    public bool _snareActive;
    [HideInInspector]
    public bool _hiHatActive;
    [HideInInspector]
    public bool _leadBassActive;
    [HideInInspector]
    public bool _atmoActive;

    // Start is called before the first frame update
    void Start()
    {
        _snareActive = false;
        _hiHatActive = false;
        _leadBassActive = false;
        _atmoActive = false;
    }

    public void SnareLayer()
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

    public void HiHatLayer()
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

    public void LeadBassLayer()
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

    public void AtmoLayer()
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
