using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Timeline;

public class AudioLight : MonoBehaviour
{


    public int _audioBand1 = 0;
    public float emissiveStrength = 0.5f;

    private SpectrumManager spectrumManager;
    Sequence tweenSeq;
    private Material _material;
    private Color color;
    private Light l;
    public float intensity = 100f;
    public bool useMarker = false;
    public bool m_onSnare = false;
    public bool m_onKick = false;
    public bool m_onHiHat = false;
    public bool m_onLeadBass = false;
    public bool m_onAtmo = false;

    public float markerIntensity = 1000;

    private bool interval = false;

    // Start is called before the first frame update
    void Start()
    {
        spectrumManager = GameObject.Find("AudioManager").GetComponent<SpectrumManager>();
        // _material = GetComponent<MeshRenderer>().material;
        //color = _material.GetColor("_EmissiveColor");
        l = GetComponent<Light>();

        if (useMarker)
        {
            if (m_onSnare)
            {
                MyEventSystem.instance.Snare += objectAction;
            }
            if (m_onKick)
            {
                MyEventSystem.instance.Kick += objectAction;
            }
            if (m_onHiHat)
            {
                MyEventSystem.instance.HiHat += objectAction;
            }
            if (m_onLeadBass)
            {
                MyEventSystem.instance.LeadBass += objectAction;
            }
            if (m_onAtmo)
            {
                MyEventSystem.instance.Atmo += objectAction;
            }
        }
        l.DOIntensity(0, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!useMarker)
            l.DOIntensity(spectrumManager.getFqBandBuffer8(_audioBand1) * intensity, 0f);
    }

    public void objectAction()
    {
        if (!interval)
        {
            l.DOIntensity(markerIntensity, 0.25f);
            interval = true;
        }
        else
        {
            l.DOIntensity(10, 0.25f);
            interval = false;
        }

    }
}
