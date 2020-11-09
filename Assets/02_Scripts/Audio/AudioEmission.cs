using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioEmission : MonoBehaviour
{

    public int _audioBand1 = 0;
    public int _audioBand2 = 7;

    public bool _enableBlueFQChannel = true; //BLUE
    public bool _enableRedFQChannel = true; //RED

    public bool _enableCustomMaterialColors = false;

    private int _bandAmount = 2;

    Sequence tweenSeq;
    private Material _material;


    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.DOFloat(100, "EmissionIntensity", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_enableBlueFQChannel)
        {
            _material.DOFloat(SpectrumManager._instance.getFqBandBuffer8(_audioBand1) * 10000, "EmissionBlueChannelIntensity", 0);
            //_material.DOFloat(1000, "EmissionBlueChannelIntensity", 0);
        }

        if (_enableRedFQChannel)
        {
              _material.DOFloat(SpectrumManager._instance.getFqBandBuffer8(_audioBand2) * 10000, "EmissionRedChannelIntensity", 0);
           // _material.DOFloat(10, "EmissionRedChannelIntensity", 0);
        }
    }
}

