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
    public PlayerInputManager inputManager;

    #region __________Vector 2&3__________

    [HideInInspector] public Vector3 currentMoveDirection, currentLookDirection;
    [HideInInspector] public Vector3 forward, right, pointToLook, currentLook;
    [HideInInspector] public Vector3 velocity = Vector3.zero;
    [HideInInspector] public Vector3 gravity = Vector3.zero;

    #endregion

    #region __________bool__________

    [HideInInspector] public bool isAiming, isGrounded = false, checkEnemy = false, isMoving = false;

    #endregion

    #region __________float__________
    [Header("Move Settings")]
    public float currentMoveSpeed = 5.0f, standardMoveSpeed;

    [Header("Dash Settings")]
    public float dashCharge, dashRechargeTime, maxDashCharge;
    public float dashForce = 1.0f, dashDuration = 0.3f, dashDistance = 7f, drag = 1f, delayTime;
    [HideInInspector] public float dashTime;

    #endregion

    #region __________other__________
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public CharacterController characterController => GetComponent<CharacterController>();
    [HideInInspector] public PlayerStatistics playerStatistics => GetComponent<PlayerStatistics>();
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public PlayerMovementSate currentState;
    PlayerMovementController standardMovement;
    DashMovementController dashController;
    //GrenadeMovementController grenadeController;
    //AttackMovementState attackController;
    //[HideInInspector] public PlayerAttack playerAttack => GetComponent<PlayerAttack>();
    //public AnimationClip clip;

    //layableGraph playableGraph;

    #endregion

    private void Awake()
    {
        standardMovement = new PlayerMovementController(this);
        dashController = new DashMovementController(this);
        inputManager.controls.Gameplay.Movement.performed += ctx => IsMoving();
        inputManager.controls.Gameplay.Movement.canceled += ctx => IsNotMoving();
        //attackController = new AttackMovementState(this);
        //grenadeController = new GrenadeMovementController(this);
    }

    private void OnDisable()
    {
        MyEventSystem.instance.SetState -= SetState;
    }

    private void Start()
    {
        SetState(PlayerMovementSate.standard);
        MyEventSystem.instance.SetState += SetState;

        //playableGraph = PlayableGraph.Create();

        //playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        //var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Run", GetComponentInChildren<Animator>());

        // Wrap the clip in a playable

        //var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        // Connect the Playable to an output

        //playableOutput.SetSourcePlayable(clipPlayable);
        standardMoveSpeed = playerStatistics.GetStatValue(StatName.Speed);
    }

    void Update()
    {
        switch (currentState)
        {
            case PlayerMovementSate.standard:
                // CheckSlope();
                standardMovement.Tick(this);
                break;
            case PlayerMovementSate.dash:
                dashController.Tick(this);
                break;
                /*case PlayerMovementSate.grenade:
                    grenadeController.Tick(this);
                    break;
                case PlayerMovementSate.attack:
                    attackController.Tick(this);
                    break;*/
        }

        Move();
        DashCooldown();
    }

    void Move()
    {
        // if (playerBody.alive)
        // {
        IsGrounded();
        velocity.y = 0;
        characterController.Move(((Vector3.Normalize(currentMoveDirection) + velocity) * currentMoveSpeed) * Time.deltaTime);
        // }
    }

    void IsMoving()
    {
        isMoving = true;
        // if (currentState == PlayerMovementSate.standard)
        // {
        //     PlayAnim();
        // }
    }

    void IsNotMoving()
    {
        isMoving = false;
        //playableGraph.Stop();
    }

    public void DashCooldown()
    {
        float timeSinceDashEnded = Time.time - dashTime;

        float perc = timeSinceDashEnded / dashRechargeTime;

        dashCharge = Mathf.Lerp(0, maxDashCharge, perc);
    }

    public void IsGrounded()
    {
        if (Physics.CheckSphere(transform.position + new Vector3(0, 1f, 0), 1.01f, groundMask, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            characterController.Move(Physics.gravity * Time.deltaTime);
        }
    }

    public void SetState(PlayerMovementSate state)
    {
        velocity = Vector3.zero;
        switch (state)
        {
            case PlayerMovementSate.dash:
                break;
            case PlayerMovementSate.standard:
                ResetMoveSpeed();
                break;
                /*case PlayerMovementSate.grenade:
                    GrenadeMoveSpeed();
                    break;*/
        }
        currentState = state;
    }

    void ResetMoveSpeed()
    {
        currentMoveSpeed = standardMoveSpeed;
    }

    public void StartDash()
    {
        if (dashCharge < 100 || !isMoving)
            return;

        SetState(PlayerMovementSate.dash);
        dashController.DashInit(this);
    }

    /*public void Attack()
    {
        currentEnemyTarget = attackController.FindTarget(this);
        attackController.StopMovement(this);
    }

    void GrenadeMoveSpeed()
    {
        currentMoveSpeed = grenadeMoveSpeed;
    }

    void PlayAnim()
    {

        // Plays the Graph.

        //playableGraph.Play();
    }*/

}
