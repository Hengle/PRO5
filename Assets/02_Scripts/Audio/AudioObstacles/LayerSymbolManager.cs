﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LayerSymbolManager : MonoBehaviour
{
    private Renderer _renderer;
   // private MaterialPropertyBlock _propBlock;

    public Boolean Kick = false;
    public Boolean Snare = false;
    public Boolean HiHat = false;

    private MaterialPropertyBlock _propBlock;
    //TODO
    //Das Script sollte das AudioObstacle fragen auf welche Spur es hört damit das Symbol automatisch zugewiesen werden kann
    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();

    }

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
          // _renderer.material.SetTextureOffset("_UnlitColorMap", new Vector2(triangle, offsetY));

        }
        else if (Snare)
        {
            //_renderer.material.SetTextureOffset("_UnlitColorMap", new Vector2(square, offsetY));
            _propBlock.SetVector("_UnlitColorMap", new Vector4(0.5f, 0.5f, 0.25f, 0.25f));
            _renderer.SetPropertyBlock(_propBlock);
        }
        else if (HiHat)
        {
          //  _renderer.material.SetTextureOffset("_UnlitColorMap", new Vector2(hexagon, offsetY));
        }
    }


}