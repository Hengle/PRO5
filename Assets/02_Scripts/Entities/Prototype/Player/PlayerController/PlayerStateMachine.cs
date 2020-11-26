using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using Debug = UnityEngine.Debug;

public enum PlayerMovementSate
{
    standard,
    dash,
    //grenade,
    //attack,
    //comboWait
}

public class PlayerStateMachine : MonoBehaviour
{
    #region __________Vector 2&3__________

    [HideInInspector] public Vector3 currentMoveDirection, currentLookDirection;
    [HideInInspector] public Vector3 forward, right, pointToLook, currentLook;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 gamepadRotate;
    [HideInInspector] public Vector2 mouseLook;
    [HideInInspector] public Vector3 velocity = Vector3.zero;
    [HideInInspector] public Vector3 gravity = Vector3.zero;

    #endregion

    #region __________bool__________

    [HideInInspector]
    public bool isAiming, mouseused, gamepadused, isGrounded = false, checkEnemy = false, isMoving = false;

    #endregion

    #region __________float__________

    // [SerializeField] private float rotationSpeed = 50f; //later used for smoothing rapid turns of the player
    [HideInInspector] public float deltaTime;
    [HideInInspector] public float time;

    public float currentMoveSpeed = 5.0f, standardMoveSpeed, dashCharge, dashRechargeTime, maxDashCharge;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;
    [HideInInspector] public float dashTime;

    #endregion

    #region __________other__________

    [HideInInspector] public GameObject dashTarget;
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public PlayerControls input;
    public Transform RayEmitter;
    [HideInInspector] public PlayerMovementSate currentState;
    PlayerMovementController standardMovement;

    DashMovementController dashController;
   
    [HideInInspector] public CharacterController characterController => GetComponent<CharacterController>();
    public CapsuleCollider selfCol;
    [SerializeField] private StatTemplate playerTemplate;
    public PlayerBody playerBody => GetComponent<PlayerBody>();

    public Transform currentEnemyTarget;
    private Plane groundPlane;

    private Camera mainCam => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    

    #endregion

    private void Awake()
    {
        input = new PlayerControls();
        //standardMovement = new PlayerMovementController(this);
        //dashController = new DashMovementController(this);
       
        input.Gameplay.Dash.performed += ctx => Dash();
        input.Gameplay.Movement.performed += ctx => IsMoving();
        input.Gameplay.Movement.canceled += ctx => IsNotMoving();
        
        
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
        //MyEventSystem.instance.SetState -= SetState;
       
    }

    private void Start()
    {
        
        //Set up for the movement
        forward = mainCam.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        Cursor.visible = true;
        //SetState(PlayerMovementSate.standard);
       // MyEventSystem.instance.SetState += SetState;

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

        GetInputValues();

        /*switch (currentState)
        {
            case PlayerMovementSate.standard:
                // CheckSlope();
                standardMovement.Tick(this);
                break;
            case PlayerMovementSate.dash:
                dashController.Tick(this);
                break;
           
        }*/

        Move(this);
        GamepadLook(this);
        MouseLook(this);
        
       // Move();
        DashCooldown();
    }

    void GetInputValues()
    {
        move = input.Gameplay.Movement.ReadValue<Vector2>();
        gamepadRotate = input.Gameplay.Rotate.ReadValue<Vector2>();
        mouseLook = input.Gameplay.Look.ReadValue<Vector2>();
    }

    /*void Move()
    {
        // if (playerBody.alive)
        // {
        IsGrounded();
        velocity.y = 0;
        characterController.Move(((Vector3.Normalize(currentMoveDirection) + velocity) * currentMoveSpeed) *
                                 Time.deltaTime);
        // }
    }*/

    void IsMoving()
    {
        isMoving = true;
        
    }

    void IsNotMoving()
    {
        isMoving = false;
        
    }

    public void DashCooldown()
    {
        float timeSinceDashEnded = time - dashTime;

        float perc = timeSinceDashEnded / dashRechargeTime;

        dashCharge = Mathf.Lerp(0, maxDashCharge, perc);
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

    public void Dash()
    {
        velocity = Vector3.zero;
        
        if (dashCharge < 100 || !isMoving)
            return;
        StartDash();
    }

    /*public void SetState(PlayerMovementSate state)
    {
        velocity = Vector3.zero;
        switch (state)
        {
           
            case PlayerMovementSate.dash:
                if (dashCharge < 100 || !isMoving)
                    return;
                StartDash();
                break;
            case PlayerMovementSate.standard:
                ResetMoveSpeed();
                break;
          
        }

        currentState = state;
    }*/

    /*void ResetMoveSpeed()
    {
        currentMoveSpeed = standardMoveSpeed;
    }*/

    public void StartDash()
    {
        dashController.DashInit(this);
    }
    

    #region Movement

    void Move(PlayerStateMachine controller)
    {
        
        IsGrounded();
        velocity.y = 0;
        characterController.Move(((Vector3.Normalize(currentMoveDirection) + velocity) * currentMoveSpeed) *
                                 Time.deltaTime);
        
        Vector2 move = controller.move;
        Vector3 direction = new Vector3(move.x, 0, move.y);

        Vector3 horizMovement = controller.right * direction.x;
        Vector3 vertikMovement = controller.forward * direction.z;

        controller.currentMoveDirection = horizMovement + vertikMovement;
    }
    
    #endregion

    #region Look direction

    void GamepadLook(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Rotate.triggered || controller.gamepadused)
        {
            controller.gamepadused = true;
            controller.mouseused = false;
            Vector2 v = controller.gamepadRotate;
            var lookRot = mainCam.transform.TransformDirection(new Vector3(v.x, 0, v.y));
            controller.pointToLook = Vector3.ProjectOnPlane(lookRot, Vector3.up);
            UpdateLookDirection(controller);
        }
    }

    void MouseLook(PlayerStateMachine controller)
    {
        if (controller.input.Gameplay.Look.triggered || controller.mouseused)
        {
            controller.gamepadused = false;
            controller.mouseused = true;
            Vector2 v = controller.mouseLook;
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
            UpdateLookDirection(controller);
        }
    }

    void UpdateLookDirection(PlayerStateMachine controller)
    {
        controller.pointToLook.y = 0;
        if (controller.pointToLook != Vector3.zero)
        {
            Quaternion newRot = Quaternion.LookRotation(controller.pointToLook);
            controller.transform.rotation = newRot;
            controller.pointToLook.y = 0;
            controller.currentLookDirection = controller.pointToLook;
            //Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotationSpeed);
        }
    }

    #endregion

   
}