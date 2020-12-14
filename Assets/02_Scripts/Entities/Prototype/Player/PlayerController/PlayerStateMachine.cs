using System.Collections;
using System.Collections.Generic;
using Ludiq.Peek;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Debug = UnityEngine.Debug;

public class PlayerStateMachine : MonoBehaviour
{
    #region __________Vector 2&3__________

    [HideInInspector] public Vector3 currentMoveDirection, currentLookDirection;
    [HideInInspector] public Vector3 forward, right, pointToLook, currentLook;
    [HideInInspector] public Vector3 velocity = Vector3.zero;
    private Vector3 dashVelocity;

    #endregion

    #region __________bool__________

    [HideInInspector] public bool isGrounded = false;
    public bool isDelaying = false;
    private bool dashPressed = false;

    #endregion

    #region __________float__________

    [Header("Move Settings")] public float currentMoveSpeed, standardMoveSpeed = 7.0f;

    [Header("Dash Settings")]
    public float dashCharge, dashRechargeTime, maxDashCharge, dashSpeed = 1.0f, dashDuration = 0.3f, delayTime;

    [HideInInspector] public float dashTime;
    private float timeStartDash, timeSinceStarted, delayCountdown;

    #endregion

    #region __________other__________

    [HideInInspector] public PlayerStatistics playerStatistics => GetComponent<PlayerStatistics>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    public PlayerInputManager inputManager;
    [HideInInspector] public CharacterController characterController => GetComponent<CharacterController>();
    [SerializeField] private StatTemplate playerTemplate;
    private Plane groundPlane;
    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    public bool isDashing, dashDelayOn = false;

    #endregion

    #region Start/Update

    private void Start()
    {
        //Set up for the movement
        forward = mainCam.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;

        currentMoveSpeed = standardMoveSpeed;

        //frametime = dashDuration;
        delayCountdown = delayTime;

        foreach (FloatReference f in playerTemplate.statList)
        {
            StatVariable s = (StatVariable) f.Variable;
            if (s.statName.ToString().Equals("Speed"))
            {
                standardMoveSpeed = s.Value;
            }
        }
    }

    void Update()
    {
        //frametime -= Time.deltaTime;
        isDashing = playerStatistics.isDashing;

        DelayUpdate();
        Move();
        UpdateLookDirection();
        DashCooldown();
    }

    #endregion

    #region Movement

    void Move()
    {
        IsGrounded();
        velocity.y = 0;
        characterController.Move(((Vector3.Normalize(currentMoveDirection) + velocity) * currentMoveSpeed) *
                                 Time.deltaTime);

        Vector2 move = inputManager.move;
        Vector3 direction = new Vector3(move.x, 0, move.y);

        Vector3 horizMovement = right * direction.x;
        Vector3 vertikMovement = forward * direction.z;

        if (!playerStatistics.isDashing)
        {
            currentMoveDirection = horizMovement + vertikMovement;
        }
    }

    public void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, 1f, 0), 1.01f, groundMask,
            QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            characterController.Move(Physics.gravity * Time.deltaTime);
        }
    }

    void UpdateLookDirection()
    {
        pointToLook = currentMoveDirection;
        pointToLook.y = 0;
        if (pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(pointToLook);
            transform.rotation = newRot;
            pointToLook.y = 0;
            currentLookDirection = pointToLook;
            //Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }

    #endregion

    #region Dash

    public void DashCooldown()
    {
        float timeSinceDashEnded = Time.time - dashTime;

        float perc = timeSinceDashEnded / dashRechargeTime;

        dashCharge = Mathf.Lerp(0, maxDashCharge, perc);
    }

    public void StartDash()
    {
        if (!dashPressed && dashCharge >= 100 && dashDelayOn)
        {
            dashPressed = true;
            dashCharge = 0;
            isDelaying = true;
            currentMoveSpeed = 0;
        }
    }
    
    public void DelayUpdate()
    {
        if (isDelaying)
        {
            delayCountdown -= Time.deltaTime;
            if (delayCountdown <= 0)
            {
                isDelaying = false;
                playerStatistics.isDashing = true;

                if (currentMoveDirection == Vector3.zero)
                {
                    currentMoveDirection = currentLookDirection;
                }
                currentMoveSpeed = standardMoveSpeed * dashSpeed;
                delayCountdown = delayTime;
                Invoke("setMovementBack", dashDuration);
            }
        }
    }
    
    public void setMovementBack()
    {
        currentMoveSpeed = standardMoveSpeed;
        dashTime = Time.time;
        playerStatistics.isDashing = false;
        dashPressed = false;
    }

    #endregion


    void PlayDeath()
    {
        
    }
    /*void DashDelay()
  {
      delayCountdown -= Time.deltaTime;
      //currentMoveDirection = Vector3.zero;
      if (delayCountdown <= 0)
      {
          velocity = Vector3.zero;
          delayCountdown = delayTime; ;
          dashDelayOn = false;
        
      }
  }*/

    /*void GamepadLook()
   {
       if (!inputManager.useMouse)
       {
           gamepadused = true;
           mouseused = false;
           Vector2 v = inputManager.move;
           var lookRot = mainCam.transform.TransformDirection(new Vector3(v.x, 0, v.y));
           pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
           UpdateLookDirection();
       }
   }
   
   void MouseLook(PlayerStateMachine controller)
   {
       if (controller.inputManager.controls.Gameplay.Look.triggered || controller.mouseused)
       {
           controller.gamepadused = false;
           controller.mouseused = true;
           Vector2 v = inputManager.mouseLook;
           //Creating a "mathematical" plane for the raycast to intersect with
           groundPlane = new Plane(Vector3.up, new Vector3(0, controller.transform.position.y, 0));
           //creating the Ray
           Ray cameraRay = mainCam.ScreenPointToRay(v);
           float rayLength;
           //checking if the raycast intersects with the plane
           if (groundPlane.Raycast(cameraRay, out rayLength))
           {
               Vector3 rayPoint = cameraRay.GetPoint(rayLength);
               //Debug.DrawLine(cameraRay.origin, rayPoint);
               controller.pointToLook = rayPoint - controller.transform.position;
           }

           UpdateLookDirection();
       }
   }*/
}