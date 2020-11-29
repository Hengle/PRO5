using System.Collections;
using System.Collections.Generic;
using Ludiq.Peek;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Debug = UnityEngine.Debug;

public enum PlayerMovementSate
{
    standard,
    dash,
}

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerInputManager inputManager;

    #region __________Vector 2&3__________

    [HideInInspector] public Vector3 currentMoveDirection, currentLookDirection;
    [HideInInspector] public Vector3 forward, right, pointToLook, currentLook;
    [HideInInspector] public Vector3 velocity = Vector3.zero;
    [HideInInspector] public Vector3 gravity = Vector3.zero;

    #endregion

    #region __________bool__________

    [HideInInspector] public bool isAiming, isGrounded = false, checkEnemy = false, isMoving = false;
    [HideInInspector] public bool gamepadused, mouseused;
    public bool isDelaying = false;


    #endregion

    #region __________float__________

    [Header("Move Settings")] public float currentMoveSpeed = 5.0f, standardMoveSpeed;

    [Header("Dash Settings")] public float dashCharge, dashRechargeTime, maxDashCharge;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;
    [HideInInspector] public float dashTime;

    #endregion

    #region __________other__________

    [HideInInspector] public GameObject dashTarget;
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public PlayerStatistics playerStatistics => GetComponent<PlayerStatistics>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");


    [HideInInspector] public CharacterController characterController => GetComponent<CharacterController>();
    public CapsuleCollider selfCol;
    [SerializeField] private StatTemplate playerTemplate;
    public PlayerBody playerBody => GetComponent<PlayerBody>();
    public DashMovementController dashController;

    public Transform currentEnemyTarget;
    private Plane groundPlane;

    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    public float time, deltaTime;
    private float timeStartDash, timeSinceStarted, actualDashDistance, frametime = 0.0f, delayCountdown;
    public bool isDashing = false, dashDelayOn = false;
    private Vector3 dashVelocity;

    #endregion

    private void Awake()
    {
        //inputManager.controls.Gameplay.Movement.performed += ctx => IsMoving();
        //inputManager.controls.Gameplay.Movement.canceled += ctx => IsNotMoving();
    }


    private void Start()
    {
        //Set up for the movement
        forward = mainCam.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
        
        frametime = dashDuration;
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
        time = Time.time;
        deltaTime = Time.deltaTime;
        frametime -= Time.deltaTime;
        isDashing = playerStatistics.isDashing;
        
        DelayUpdate();
        Move();
        UpdateLookDirection();
        DashCooldown();
    }


    void IsMoving()
    {
        isMoving = true;
    }

    void IsNotMoving()
    {
        isMoving = false;
    }

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

    public void DashCooldown()
    {
        float timeSinceDashEnded = Time.time - dashTime;

        float perc = timeSinceDashEnded / dashRechargeTime;

        dashCharge = Mathf.Lerp(0, maxDashCharge, perc);
    }
    void DashDelay()
    {
        delayCountdown -= Time.deltaTime;
        //currentMoveDirection = Vector3.zero;
        if (delayCountdown <= 0)
        {
            velocity = Vector3.zero;
            delayCountdown = delayTime; ;
            dashDelayOn = false;
          
        }
    }
    
    public void StartDash()
    {
        if (dashCharge >= 100){
            standardMoveSpeed = currentMoveSpeed;
            currentMoveSpeed = 0;
            isDelaying = true;
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
                currentMoveSpeed = currentMoveSpeed * dashDistance;
                dashCharge = 0;
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