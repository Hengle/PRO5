using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PlayerStateMachine playerController;
    public PowerUp _currentPowerUp = null;


    private void Start()
    {
        MyEventSystem.instance.powerupCollected += StorePowerUp;
    }

    private void OnDisable()
    {
        MyEventSystem.instance.powerupCollected -= StorePowerUp;
    }


    public void StorePowerUp(PowerUp powerup)
    {
        Debug.Log(string.Format("Stored {0}", powerup));
        _currentPowerUp = powerup;
    }


    public void ActivatePowerUp()
    {
        if (_currentPowerUp != null)
        {
            _currentPowerUp.Activate(playerController);
            // Destroy(_currentPowerUp.gameObject);
            _currentPowerUp = null;
        }
    }
}
