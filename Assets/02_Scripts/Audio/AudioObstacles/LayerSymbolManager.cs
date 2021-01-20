using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LayerSymbolManager : MonoBehaviour
{
    private Renderer _renderer;
   // private MaterialPropertyBlock _propBlock;

    public Boolean Kick = false;
    public Boolean Snare = false;
    public Boolean HiHat = false;

    public Material Triangle;
    public Material Square;
    public Material Hexagon;

    private List<AudioObstacle> _audioObstacles;

    //TODO
    //Das Script sollte das AudioObstacle fragen auf welche Spur es hört damit das Symbol automatisch zugewiesen werden kann


    void Start()
    {
        askAudioObstacle();
        //Offsets
        float triangle = 0.02f;
        float square = 0.35f;
        float hexagon = 0.68f;

        _renderer = GetComponent<Renderer>();

        if (Kick)
        {
            _renderer.material = Square;

        }
        else if (Snare)
        {
            _renderer.material = Hexagon;
        }
        else if (HiHat)
        {
            _renderer.material = Triangle;
        }
    }

    void askAudioObstacle()
    {
        //hi obstacle which layer are you hearing
        _audioObstacles = new List<AudioObstacle>(gameObject.GetComponentsInParent<AudioObstacle>());
        if (_audioObstacles[0].m_onKick) Kick = true;
        else if (_audioObstacles[0].m_onSnare) Snare = true;
        else if (_audioObstacles[0].m_onHiHat) HiHat = true;
    }

    
}
