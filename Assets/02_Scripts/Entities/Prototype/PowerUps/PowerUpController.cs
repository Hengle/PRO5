using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PlayerStateMachine playerController;
    public PowerUp _currentPowerUp = null;
    public UIPrototype ui;

    public SoundEffectsList PU_activation;

    private void Start()
    {
        MyEventSystem.instance.powerupCollected += StorePowerUp;
        GlobalEventSystem.instance.onRestart += StartLoad;
    }
    private void OnDisable()
    {
        MyEventSystem.instance.powerupCollected -= StorePowerUp;
        GlobalEventSystem.instance.onRestart -= StartLoad;
    }

    void StartLoad()
    {
        if (_currentPowerUp != null)
            ui.DisablePowerUpSymbol(_currentPowerUp);
        _currentPowerUp = null;
    }

    public void StorePowerUp(PowerUp powerup)
    {
        if (_currentPowerUp != null)
        {
            ui.DisablePowerUpSymbol(_currentPowerUp);
        }


        PU_activation.PlayEffect(false, "powerup_pickup");
        Debug.Log(string.Format("Stored {0}", powerup));
        _currentPowerUp = powerup;
        powerup.transform.parent = playerController.transform;
        ui.EnablePowerUpSymbol(powerup);
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
            ui.DisablePowerUpSymbol(_currentPowerUp);
            _currentPowerUp = null;
        }
    }
}
