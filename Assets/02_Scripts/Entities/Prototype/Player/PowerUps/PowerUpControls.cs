using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpControls : MonoBehaviour
{

    public KeyCode activatePowerUp = KeyCode.X;

    public UnityEvent activateEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(activatePowerUp))
        {
            activateEvent.Invoke();
        }
    }
}
