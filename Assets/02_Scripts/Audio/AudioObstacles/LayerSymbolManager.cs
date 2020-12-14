using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSymbolManager : MonoBehaviour
{
    private Renderer _renderer;
   // private MaterialPropertyBlock _propBlock;

    public Boolean Kick = false;
    public Boolean Snare = false;
    public Boolean HiHat = false;


    //TODO
    //Das Script sollte das AudioObstacle fragen auf welche Spur es hört damit das Symbol automatisch zugewiesen werden kann


    void Start()
    {

        //Offsets
        float triangle = 0.02f;
        float square = 0.35f;
        float hexagon = 0.68f;

        _renderer = GetComponent<Renderer>();

        float offsetY = _renderer.material.GetTextureOffset("_UnlitColorMap").y;

        if (Kick)
        {
            _renderer.material.SetTextureOffset("_UnlitColorMap", new Vector2(triangle, offsetY));
        }
        else if (Snare)
        {
            _renderer.material.SetTextureOffset("_UnlitColorMap", new Vector2(square, offsetY));
        }
        else if (HiHat)
        {
            _renderer.material.SetTextureOffset("_UnlitColorMap", new Vector2(hexagon, offsetY));
        }
    }


}
