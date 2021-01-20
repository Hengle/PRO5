using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PlayerStateMachine playerController;
    public PowerUp _currentPowerUp = null;

    public SoundEffectsList PU_activation;

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

        PU_activation.PlayEffect(false, "powerup_pickup");
        Debug.Log(string.Format("Stored {0}", powerup));
        _currentPowerUp = powerup;
        powerup.transform.parent = playerController.transform;
    }


    public void ActivatePowerUp()
    {
        if (_currentPowerUp != null)
        {
            PU_activation.PlayEffect(false, "powerup_activate");
            if (_currentPowerUp.soundEffect != null)
                _currentPowerUp.soundEffect.Play();

            _currentPowerUp.Activate(playerController);
            // Destroy(_currentPowerUp.gameObject);
            _currentPowerUp = null;
        }
    }
}
