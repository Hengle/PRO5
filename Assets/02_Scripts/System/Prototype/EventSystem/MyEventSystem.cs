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
    public event System.Action LeadBass;
    public event System.Action Atmo;
    public event System.Action Deactivate;

    public event System.Action AimGrenade;
    public event System.Action ThrowGrenade;
    public event System.Action Explode;


    //Events for Enemy managment
    public event Action<EnemyBody> onEnemyStart;
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

    public event System.Action onUpdateNavMesh;

    public static MyEventSystem instance;

    private void OnEnable()
    {
        instance = this;
    }

    public void OnUpdateNavMesh()
    {
        onUpdateNavMesh?.Invoke();
    }

    public void WaveDefeated()
    {
        //Questionmark checks if event has subscribers
        waveDefeated?.Invoke();
    }

    public void OnEnemyStart(EnemyBody enemy)
    {
        onEnemyStart?.Invoke(enemy);
    }
    public void GoalDestroyed()
    {
        //Questionmark checks if event has subscribers
        goalDestroyed?.Invoke();
    }


    public void OnSnare()
    {
        Snare?.Invoke();

    }

    public void OnHiHat()
    {
        HiHat?.Invoke();

    }

    public void OnKick()
    {
        Kick?.Invoke();
    }

    public void OnLeadBass()
    {
        LeadBass?.Invoke();
    }

    public void OnAtmo()
    {

        Atmo?.Invoke();
    }


    public void OnDeactivate()
    {
        Deactivate?.Invoke();
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

    public void OnTeleportPlayer(Transform player)
    {
        teleportPlayer?.Invoke(player);
    }

    #endregion TeleportEventhandler

    public void OnEnemyDeath(EnemyBody enemy)
    {
        onEnemyDeath?.Invoke(enemy);
    }

    /*public void OnSetState(PlayerMovementSate state)
    {
        SetState?.Invoke(state);
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
