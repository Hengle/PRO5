﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
/*public class PlayerAttack : MonoBehaviour
{
    PlayerStateMachine playerMovement;

    [SerializeField] public List<Weapons> weapons = new List<Weapons>(2);
    public Weapons currentWeapon;
    public int currentWeaponCounter = 0;
    public Transform weaponPoint;
    public Skills currentActiveSkill;
    public bool skillIsActive;

    [SerializeField] public List<Skills> skills = new List<Skills>();

    public int comboCounter = 0;

    public GameObject grenadePrefab;
    public GameObject targetPrefab;
    public GameObject target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public float greandeCooldown = 10f;
    public float currentGCooldown = 10f;
    private float changeWeaponTimer = 0;

    bool canThrowG = true;
    [SerializeField] private float moveSpeed = 5f;
    //  public Vector3 currentDirection;


    public GameObject AudioPeer;
    // public static FMODUnity.StudioEventEmitter emitter;
    private static PlayerMovementSate movementState => GameObject.FindGameObjectWithTag("Player")
        .GetComponent<PlayerStateMachine>().currentState;

    // private void OnEnable()
    // {
    //     GameManager.instance.initAll += GetEmitter;
    //     GameManager.instance.deInitAll += DisbandEmiter;
    // }

    // private void OnDisable()
    // {
    //     GameManager.instance.initAll -= GetEmitter;
    //     GameManager.instance.deInitAll -= DisbandEmiter;
    // }


    // void DisbandEmiter()
    // {
    //     emitter = null;
    // }
    // void GetEmitter()
    // {
    //     emitter = FMODAudioPeer._instance.gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
    // }
    private void Awake()
    {
        playerMovement = GetComponent<PlayerStateMachine>();
        StartCoroutine(InitInput());
    }

    IEnumerator InitInput()
    {
        yield return new WaitForEndOfFrame();
        // playerMovement.input.Gameplay.GrenadeThrow.performed += rt => AimMove();
        // playerMovement.input.Gameplay.GrenadeReleaser.performed += rt => GrenadeThrow();
        // playerMovement.input.Gameplay.Skill1.performed += rt => Skill(0);
        // playerMovement.input.Gameplay.Skill2.performed += rt => Skill(1);
        // playerMovement.input.Gameplay.Skill3.performed += rt => Skill(2);
        playerMovement.input.Gameplay.WeaponSwitch.performed += rt => ChangeWeapon();
    }



    private void Reset()
    {
    }

    void Start()
    {
        // if (emitter == null)
        //     GetEmitter();

        currentWeapon = weapons[currentWeaponCounter];
        // currentWeapon.Equip(weaponPoint);


        foreach (Skills skill in skills)
        {
            skill.current = 0;
        }
    }

    public Skills SkillInit(string name, float current, float max, float timer)
    {
        Skills skillTemp = ScriptableObject.CreateInstance<Skills>();
        skillTemp.skillName = name;
        skillTemp.current = current;
        skillTemp.max = max;
        skillTemp.timer = timer;

        return skillTemp;
    }

    // void Skill(int id)
    // {
    //     Skills temp = skills[id];
    //     if (temp.current == temp.max && currentActiveSkill == null)
    //     {
    //         if (temp.isActive & (temp.current <= 0 || temp.current >= temp.max))
    //         {
    //             temp.isActive = false;
    //             MyEventSystem.instance.OnSkill(temp);
    //             StartCoroutine(Timer(temp, id));
    //         }
    //         else if (!temp.isActive)
    //         {
    //             MyEventSystem.instance.OnSkill(temp);
    //             StartCoroutine(Timer(temp, id));
    //         }
    //         // foreach (Skills skill in skills)
    //         // {
    //         //     skill.isActive = false;
    //         // }
    //     }
    // }

    // IEnumerator Timer(Skills temp, int id)
    // {
    //     temp.isActive = true;
    //     currentActiveSkill = temp;
    //     emitter.SetParameter(temp.skillName, temp.activeValue);
    //     temp.current = 0;
    //     yield return new WaitForSeconds(temp.timer);

    //     temp.isActive = false;
    //     currentActiveSkill = null;
    //     //Debug.Log("hi");
    //     MyEventSystem.instance.OnSkillDeactivation(temp);
    //     emitter.SetParameter(temp.skillName, temp.deactiveValue);
    //     yield return null;
    // }

    // public void GrenadeThrow()
    // {
    //     if (movementState.Equals(PlayerMovementSate.grenade))
    //     {
    //         Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    //         GameObject grenade = Instantiate(grenadePrefab, pos, transform.rotation);
    //         StartCoroutine(SimulateProjectile(grenade));
    //         StartCoroutine(GrenadeCountdown());
    //         MyEventSystem.instance.OnSetState(PlayerMovementSate.standard);
    //         Destroy(target);
    //     }
    // }

    // public void AimMove()
    // {
    //     if (movementState.Equals(PlayerMovementSate.standard) & canThrowG)
    //     {
    //         MyEventSystem.instance.OnSetState(PlayerMovementSate.grenade);
    //         target = Instantiate(targetPrefab, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z),
    //             transform.rotation);
    //     }
    // }

    IEnumerator GrenadeCountdown()
    {
        canThrowG = false;
        currentGCooldown = 0;
        while (currentGCooldown <= greandeCooldown)
        {
            yield return new WaitForSeconds(0.1f);
            currentGCooldown += 0.1f;
        }
        canThrowG = true;
    }
    IEnumerator SimulateProjectile(GameObject grenade)
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        //grenade.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(grenade.transform.position, target.transform.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        grenade.transform.rotation = Quaternion.LookRotation(target.transform.position - grenade.transform.position);

        float elapseTime = 0;

        while (elapseTime < flightDuration)
        {
            if (grenade == null)
            {
                break;
            }

            grenade.transform.Translate(0, (Vy - (gravity * elapseTime)) * Time.deltaTime, Vx * Time.deltaTime);

            elapseTime += Time.deltaTime;

            yield return null;
        }

        if (elapseTime >= flightDuration || grenade == null)
        {
            if (grenade != null)
            {
                // MyEventSystem.instance.OnExplode();
            }
            yield return null;
        }
    }

    //simple script to change between the weapons
    void ChangeWeapon()
    {

        if (movementState == PlayerMovementSate.standard && changeWeaponTimer > 1)
        {
            currentWeaponCounter++;
            if (currentWeaponCounter >= weapons.Count)
            {
                currentWeaponCounter = 0;
            }

            if (currentWeapon != null)
            {
                currentWeapon.Unequip();
            }

            currentWeapon = weapons[currentWeaponCounter];
            // currentWeapon.Equip(weaponPoint);
            changeWeaponTimer = 0;
        }


    }

    private void Update()
    {
        changeWeaponTimer += Time.deltaTime;
    }


    public Skills GetCurrentSkill()
    {
        return currentActiveSkill;
    }
}*/