using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LayerSymbolManager : MonoBehaviour
{
    private Renderer _renderer;

    public Boolean Kick = false;
    public Boolean Snare = false;
    public Boolean HiHat = false;
    public Boolean LeadBass = false;
    public Boolean Atmo = false;

    public Material HiHatMat;
    public Material LeadBassMat;
    public Material AtmoMat;
    public Material SnareMat;
    public Material KickMat;

    private List<AudioObstacle> _audioObstacles;

  

    void Start()
    {
        askAudioObstacle();

        _renderer = GetComponent<Renderer>();

        if (Kick)
        {
            _renderer.material = KickMat;

        }
        else if (Snare)
        {
            _renderer.material = SnareMat;
        }
        else if (HiHat)
        {
            _renderer.material = HiHatMat;
        }
        else if (LeadBass)
        {
            _renderer.material = LeadBassMat;
        }
        else if (Atmo)
        {
            _renderer.material = AtmoMat;
        }
    }

    void askAudioObstacle()
    {
        //hi obstacle which layer are you hearing
        int index = 0;
        if (transform.tag == "Plate")
        {
            _audioObstacles = new List<AudioObstacle>();
            _audioObstacles.Add(transform.GetComponent<AudioObstacle>());
        }
        else
        {
            _audioObstacles = new List<AudioObstacle>(gameObject.GetComponentsInParent<AudioObstacle>());

        }

        if (transform.tag == "Mover")
        {
            index = 1;
        }
        else if (transform.tag == "Turret")
        {
            index = 0;
        }
        switch (_audioObstacles[index].ListeningOnLayer)
        {
            case musicEvent.Snare:
                Snare = true;
                break;
            case musicEvent.HiHat:
                HiHat = true;
                break;
            case musicEvent.LeadBass:
                LeadBass = true;
                break;
            case musicEvent.Kick:
                Kick = true;
                break;
            case musicEvent.Atmo:
                Atmo = true;
                break;
        }
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
                // code here for Editor only
        
            askAudioObstacle();

            if (Kick)
            {
                _renderer.material = KickMat;
            }
            else if (Snare)
            {
                _renderer.material = SnareMat;
            }
            else if (HiHat)
            {
                _renderer.material = HiHatMat;
            }
            else if (LeadBass)
            {
                _renderer.material = LeadBassMat;
            }
            else if (Atmo)
            {
                _renderer.material = AtmoMat;
            }
            Resetbools();
        }
    }

    private void Resetbools()
    {
        Kick = false;
        Snare = false;
        HiHat = false;
        LeadBass = false;
        Atmo = false;
    }

}
