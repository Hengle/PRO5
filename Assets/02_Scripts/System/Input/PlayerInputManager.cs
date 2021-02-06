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
    [SerializeField] SkillController skillController;

    private void OnEnable()
    {
        GlobalEventSystem.instance.onLoadFinish += EnableControls;
        EnableControls();
    }

    private void OnDisable()
    {
        GlobalEventSystem.instance.onLoadFinish -= EnableControls;
        DisableControls();
    }

    public void EnableControls()
    {
        controls.Enable();
    }

    public void DisableControls()
    {
        controls.Disable();
    }
    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.performed += ctx => playerController.MoveAnim(true);
        controls.Gameplay.Movement.canceled += ctx => playerController.MoveAnim(false);
        controls.Gameplay.Rotate.performed += ctx => gamepadRotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.performed += ctx => mouseLook = ctx.ReadValue<Vector2>();

        if (playerController != null)
            controls.Gameplay.Dash.performed += ctx => playerController.StartDash();

        if (powerUpController != null)
            controls.Gameplay.ActivatePowerUp.performed += ctx => powerUpController.ActivatePowerUp();

        if (musicLayerController != null)
        {
            controls.Gameplay.Skill1.performed += ctx => musicLayerController.LayerSkill(ref musicLayerController._snareActive, "SnareLayer", 1);
            controls.Gameplay.Skill2.performed += ctx => musicLayerController.LayerSkill(ref musicLayerController._hiHatActive, "HiHatLayer", 1);
            controls.Gameplay.Skill3.performed += ctx => musicLayerController.LayerSkill(ref musicLayerController._leadBassActive, "LeadBassLayer", 1);
            controls.Gameplay.Skill4.performed += ctx => musicLayerController.LayerSkill(ref musicLayerController._atmoActive, "AtmoLayer", 1);


        }

        if (skillController != null)
        {
            controls.Gameplay.Charge.performed += ctx => skillController.chargeIsPressed(true);
            controls.Gameplay.Charge.canceled += ctx => skillController.chargeIsPressed(false);
        }

        controls.Gameplay.Movement.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Rotate.canceled += ctx => gamepadRotate = Vector2.zero;
    }
}
