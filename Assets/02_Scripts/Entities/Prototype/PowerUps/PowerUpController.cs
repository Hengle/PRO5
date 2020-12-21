using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    public PowerUp _currentPowerUp = null;


    private void Start()
    {
        MyEventSystem.instance.powerupCollected += StorePowerUp;
        MyEventSystem.instance.currentPowerupActivated += ActivatePowerUp;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.powerupCollected -= StorePowerUp;
        MyEventSystem.instance.currentPowerupActivated -= ActivatePowerUp;
    }


    public void StorePowerUp(PowerUp powerup)
    {
        Debug.Log(string.Format("Stored {0}", powerup));
        _currentPowerUp = powerup;
        ActivatePowerUp();
    }


    public void ActivatePowerUp()
    {
        if (_currentPowerUp != null)
        {
            _currentPowerUp.Activate();
            _currentPowerUp = null;
        }
       

    }
}
