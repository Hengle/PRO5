using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrototype : MonoBehaviour
{

    public MusicLayerController _controller;
    public Canvas canvas;
    private Image[] imgArray;

    Color activeColor = Color.cyan;
    Color unactiveColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        imgArray = canvas.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller._snareActive)
        {
            imgArray[3].DOColor(activeColor, 0.1f);
        }
        else
        {
            imgArray[3].DOColor(unactiveColor, 0.1f);
        }

        if (_controller._hiHatActive)
        {
            imgArray[2].DOColor(activeColor, 0.1f);
        }
        else
        {
            imgArray[2].DOColor(unactiveColor, 0.1f);
        }

        if (_controller._atmoActive)
        {
            imgArray[0].DOColor(activeColor, 0.1f);
        }
        else
        {
            imgArray[0].DOColor(unactiveColor, 0.1f);
        }

        if (_controller._leadBassActive)
        {
            imgArray[1].DOColor(activeColor, 0.1f);
        }
        else
        {
            imgArray[1].DOColor(unactiveColor, 0.1f);
        }
    }
}
