using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioEmission : MonoBehaviour
{

    public int _audioBand1 = 0;
    public float emissiveStrength = 0.5f;
   // public int _audioBand2 = 7;

    /*
    public bool _enableBlueFQChannel = true; //BLUE
    public bool _enableRedFQChannel = true; //RED
    

    public bool _enableCustomMaterialColors = false;
    */

    private SpectrumManager spectrumManager;
    Sequence tweenSeq;
    private Material _material;
    private Color color;


    // Start is called before the first frame update
    void Start()
    {
        spectrumManager = GameObject.Find("AudioManager").GetComponent<SpectrumManager>();
        _material = GetComponent<MeshRenderer>().material;
        color = _material.GetColor("_EmissiveColor");
        //_material.DOFloat(1000, "_EmissionIntensity", 0);
    }

    // Update is called once per frame
    void Update()
    {

       //_material.DOFloat(SpectrumManager._instance.getFqBandBuffer8(_audioBand1) * 10000, "_EmissiveColor", 0);

        _material.DOVector(color * spectrumManager.getFqBandBuffer8(_audioBand1) * emissiveStrength, "_EmissiveColor", 0);

        /*
        if (_enableBlueFQChannel)
        {
            _material.DOFloat(SpectrumManager._instance.getFqBandBuffer8(_audioBand1) * 10000, "EmissionBlueChannelIntensity", 0);
        }

        if (_enableRedFQChannel)
        {
            _material.DOFloat(SpectrumManager._instance.getFqBandBuffer8(_audioBand2) * 10000, "EmissionRedChannelIntensity", 0);
        }
        */
    }
}

