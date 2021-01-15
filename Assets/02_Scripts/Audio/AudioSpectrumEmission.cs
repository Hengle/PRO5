using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioSpectrumEmission : MonoBehaviour
{

    public int _audioBand1 = 0;
    public float emissiveStrength = 0.5f;

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
    }

    // Update is called once per frame
    void Update()
    {
        _material.DOVector(color * spectrumManager.getFqBandBuffer8(_audioBand1) * emissiveStrength, "_EmissiveColor", 0);
    }
}

