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


    private void OnEnable()
    {
        // GlobalEventSystem.instance.onLoadFinish += StartLoad;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.GetType());
        spectrumManager = GameObject.Find("AudioManager").GetComponent<SpectrumManager>();
        _material = GetComponent<MeshRenderer>().material;
        color = _material.GetColor("_EmissiveColor");
        if (spectrumManager == null)
            Debug.Log("No spectrummanager");
    }

    // Update is called once per frame
    void Update()
    {

        _material.DOVector(color * SpectrumManager._instance.getFqBandBuffer8(_audioBand1) * emissiveStrength, "_EmissiveColor", 0);

    }
}

