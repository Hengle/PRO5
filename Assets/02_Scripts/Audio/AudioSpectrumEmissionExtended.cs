using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioSpectrumEmissionExtended : MonoBehaviour
{

    public int _audioBand1 = 0;
    public float emissiveStrength = 0.5f;
    public bool onAtmo = false;
    //public bool onLeadBass = false;
    bool startEmission = false;
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

        if (onAtmo)
        {
            MyEventSystem.instance.Atmo += StartEmission;
        }
        /*
        else if (onLeadBass)
        {
            MyEventSystem.instance.LeadBass += StartEmission;
        }
        */
    }



    void StartEmission()
    {
        if (startEmission)
        {
            startEmission = false;
        }
        else
        {
            startEmission = true;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (startEmission)
        {
            _material.DOVector(color * spectrumManager.getFqBandBuffer8(_audioBand1) * emissiveStrength, "_EmissiveColor", 0.25f);
        } else
        {
            _material.DOVector(color, "_EmissiveColor", 0.25f);
        }
    }
}
