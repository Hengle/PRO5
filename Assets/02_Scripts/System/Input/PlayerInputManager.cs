using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public bool useMouse = true;
    public PlayerControls controls;
    public Vector2 move;
    public Vector2 gamepadRotate;
    public Vector2 mouseLook;
    [SerializeField] PlayerStateMachine playerController;
    [SerializeField] PowerUpController powerUpController;
    [SerializeField] MusicLayerController musicLayerController;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.performed += ctx => gamepadRotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.performed += ctx => mouseLook = ctx.ReadValue<Vector2>();

        if (playerController != null)
            controls.Gameplay.Dash.performed += ctx => playerController.StartDash(); ;

        if (powerUpController != null)
            controls.Gameplay.ActivatePowerUp.performed += ctx => powerUpController.ActivatePowerUp();

        if (musicLayerController != null)
        {
            controls.Gameplay.Skill1.performed += ctx => musicLayerController.SnareLayer();
            controls.Gameplay.Skill2.performed += ctx => musicLayerController.HiHatLayer();
            controls.Gameplay.Skill3.performed += ctx => musicLayerController.LeadBassLayer();
            controls.Gameplay.Skill4.performed += ctx => musicLayerController.AtmoLayer();
        }


        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Rotate.canceled += ctx => gamepadRotate = Vector2.zero;
    }
}
