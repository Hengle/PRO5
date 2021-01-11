using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyEventSystem : MonoBehaviour
{
    public event Action<IHasHealth, float> Attack;
    // public event Action<Skills> ActivateSkill;
    // public event Action<Skills> DeactivateSkill;

    //public event Action<PlayerMovementSate> SetState;

    //Events die von der Musik ausgelöst werden
    public event System.Action Snare;
    public event System.Action Kick;
    public event System.Action HiHat;
    public event System.Action Deactivate;

    public event System.Action AimGrenade;
    public event System.Action ThrowGrenade;
    public event System.Action Explode;


    //Events for Enemy managment
    public event Action<EnemyBody> onEnemyDeath;
    public event Action<EnemyBody> activateAI;


    public event System.Action goalDestroyed;
    public event System.Action waveDefeated;


    #region PowerupEvents

    public event Action<PowerUp> powerupCollected;

    #endregion PowerupEvents

    #region TeleportEvents
    public event Action<Transform> teleportPlayer;

    #endregion TeleportsEvents


    public static MyEventSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void WaveDefeated()
    {
        //Questionmark checks if event has subscribers
        waveDefeated?.Invoke();
    }

    public void GoalDestroyed()
    {
        //Questionmark checks if event has subscribers
        goalDestroyed?.Invoke();
    }


    public void OnSnare()
    {
        // if (Snare == null)
        // {
        //     Debug.LogError("No Snare event");
        // }
        // else
        // {
        Snare?.Invoke();
        // }
    }

    public void OnHiHat()
    {
        // if (HiHat == null)
        // {
        //     Debug.LogError("No HiHat event");
        // }
        // else
        // {
        HiHat?.Invoke();
        // }
    }

    public void OnKick()
    {
        //     if (Kick == null)
        //     {
        //         Debug.LogError("No OnKick event");
        //     }
        //     else
        //     {
        Kick?.Invoke();
        // }
    }

    public void OnDeactivate()
    {
        // if (Deactivate == null)
        // {
        //     Debug.Log("KickEvent has no subscriber");
        // }
        // else
        // {
        Deactivate?.Invoke();
        // }
    }

    public void OnAttack(IHasHealth entity, float basedmg)
    {
        Attack?.Invoke(entity, basedmg);
    }
    public void ActivateAI(EnemyBody enemy)
    {
        activateAI?.Invoke(enemy);
    }


    #region PowerupEventhandler

    public void OnPowerupCollected(PowerUp powerup)
    {
        powerupCollected?.Invoke(powerup);
    }

    #endregion PowerupEventhandler

    #region TeleportEventhandler

    public void OnTeleportPlayer(Transform targetPosition)
    {
        teleportPlayer?.Invoke(targetPosition);
    }

    #endregion TeleportEventhandler



    /*public void OnSetState(PlayerMovementSate state)
    {
        SetState?.Invoke(state);
    }
    
   public void OnEnemyDeath(EnemyBody enemy)
   {
       if (onEnemyDeath != null)
           onEnemyDeath(enemy);
   }



        
 



    public void OnSkill(Skills skill)
    {
        if (ActivateSkill != null)
        {
            Debug.Log("Skill: " + skill.name + " activated");
            ActivateSkill(skill);
        }
    }


    public void OnSkillDeactivation(Skills skill)
    {
        if (DeactivateSkill != null)
        {
            Debug.Log("Skill: " + skill.name + " deactivated");
            DeactivateSkill(skill);
        }
    }
   */


}