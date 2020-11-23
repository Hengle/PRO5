using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    private PowerUp _currentPowerUp = null;


    public void StorePowerUp(PowerUp powerup)
    {
        Debug.Log(string.Format("Stored {0}", powerup));
        _currentPowerUp = powerup;
        ActivatePowerUp();
    }


    public void ActivatePowerUp()
    {
        _currentPowerUp.Activate();
        _currentPowerUp = null;
    }
}
