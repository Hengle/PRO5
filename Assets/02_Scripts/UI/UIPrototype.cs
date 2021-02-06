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
    public Image[] skills;
    public Image[] powerup;

    Color activeColorCyan = Color.cyan;
    Color unactiveColorCyan = new Color(0, 0.33f, 0.35f);

    Color activeColorYellow = Color.yellow;
    Color unactiveColorYellow = new Color(0.66f, 0.64f, 0.26f);

    Color powerUpSymbolEnabled = new Color(1, 1, 1, 1);
    Color powerUpSymbolDisabled = new Color(1, 1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        // skills = canvas.GetComponentsInChildren<Image>();
        skills[0].DOColor(Color.white, 0.1f);

       //MyEventSystem.instance.powerupCollected += EnablePowerUpSymbol;
    }


    public void EnablePowerUpSymbol(PowerUp powerUp)
    {

            switch (powerUp.powerupName)
        {
            case PowerupNames.Speedboost:
                {
                    powerup[2].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
            case PowerupNames.Knockback:
                {
                    powerup[0].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
            case PowerupNames.Shield:
                {
                    powerup[1].DOColor(powerUpSymbolEnabled, 0.1f);
                }
                break;
            case PowerupNames.Stun:
                {
                    powerup[3].DOColor(powerUpSymbolEnabled, 0.1f);
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
                    powerup[2].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
            case PowerupNames.Knockback:
                {
                    powerup[0].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
            case PowerupNames.Shield:
                {
                    powerup[1].DOColor(powerUpSymbolDisabled, 0.1f);
                }
                break;
            case PowerupNames.Stun:
                {
                    powerup[3].DOColor(powerUpSymbolDisabled, 0.1f);
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
            skills[1].DOColor(activeColorCyan, 0.1f);
        }
        else
        {
            skills[1].DOColor(unactiveColorCyan, 0.1f);
        }

        if (_controller._hiHatActive)
        {
            skills[2].DOColor(activeColorCyan, 0.1f);
        }
        else
        {
            skills[2].DOColor(unactiveColorCyan, 0.1f);
        }

        if (_controller._atmoActive)
        {
            skills[4].DOColor(activeColorYellow, 0.1f);
        }
        else
        {
            skills[4].DOColor(unactiveColorYellow, 0.1f);
        }

        if (_controller._leadBassActive)
        {
            skills[3].DOColor(activeColorYellow, 0.1f);
        }
        else
        {
            skills[3].DOColor(unactiveColorYellow, 0.1f);
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
