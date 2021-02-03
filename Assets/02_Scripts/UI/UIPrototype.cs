using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrototype : MonoBehaviour
{

    public MusicLayerController _controller;
    public PowerUpController _puController;
    public Canvas canvas;
    private Image[] imgArray;

    Color activeColorCyan = Color.cyan;
    Color unactiveColorCyan = new Color(0, 0.33f, 0.35f);

    Color activeColorYellow = Color.yellow;
    Color unactiveColorYellow = new Color(0.66f, 0.64f, 0.26f);

    Color powerUpSymbolEnabled = new Color(1, 1, 1, 1);
    Color powerUpSymbolDisabled = new Color(1, 1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        imgArray = canvas.GetComponentsInChildren<Image>();
        imgArray[2].DOColor(Color.white, 0.1f);

       //MyEventSystem.instance.powerupCollected += EnablePowerUpSymbol;
    }


    public void EnablePowerUpSymbol(PowerUp powerUp)
    {

            switch (powerUp.powerupName)
        {
            case PowerupNames.Speedboost:
                {
                    imgArray[7].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
            case PowerupNames.Knockback:
                {
                    imgArray[5].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
            case PowerupNames.Shield:
                {
                    imgArray[6].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
            case PowerupNames.Stun:
                {
                    imgArray[8].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
        }
    }
    public void DisablePowerUpSymbol(PowerUp powerUp)
    {
        switch (powerUp.powerupName)
        {
            case PowerupNames.Speedboost:
                {
                    imgArray[7].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
            case PowerupNames.Knockback:
                {
                    imgArray[5].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
            case PowerupNames.Shield:
                {
                    imgArray[6].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
            case PowerupNames.Stun:
                {
                    imgArray[8].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_controller._snareActive)
        {
            //Color _emissionColor = imgArray[0].material.GetColor("_EmissiveColor");
            imgArray[0].DOColor(activeColorCyan, 0.1f);
        }
        else
        {
            imgArray[0].DOColor(unactiveColorCyan, 0.1f);
        }

        if (_controller._hiHatActive)
        {
            imgArray[1].DOColor(activeColorCyan, 0.1f);
        }
        else
        {
            imgArray[1].DOColor(unactiveColorCyan, 0.1f);
        }

        if (_controller._atmoActive)
        {
            imgArray[4].DOColor(activeColorYellow, 0.1f);
        }
        else
        {
            imgArray[4].DOColor(unactiveColorYellow, 0.1f);
        }

        if (_controller._leadBassActive)
        {
            imgArray[3].DOColor(activeColorYellow, 0.1f);
        }
        else
        {
            imgArray[3].DOColor(unactiveColorYellow, 0.1f);
        }



        /*
         * ------
        if (_puController._currentPowerUp.powerupName.Equals("Speedboost"))
        {
            Debug.Log("Worked");
            imgArray[5].DOColor(new Color(1, 1, 1, 1), 0.1f);
        }
        else
        {
           
        }
        *--------
        */




        /*
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
        */
    }
}
