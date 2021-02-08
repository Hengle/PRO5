using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatistics : StatisticController, IHasHealth
{
    public FloatVariable currentHealth;
    public FloatVariable shieldValue;
    public bool isDashing = false;
    public bool alive = true;
    private void OnEnable()
    {
        GlobalEventSystem.instance.onLoadFinish += StartLoad;
    }
    protected override void InitStats()
    {
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }

        currentHealth.Value = GetStatValue(StatName.MaxHealth);
    }

    void StartLoad()
    {
        currentHealth.Value = GetStatValue(StatName.MaxHealth);
        alive = true;
    }


    void CheckHealth()
    {
        if (currentHealth.Value <= 0)
        {
            OnDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        GetComponent<EffectManager>().PlaySoundEffect("hurt");
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        float newDamage = damage * damage / (damage + GetStatValue(StatName.Defense));
        currentHealth.Value -= Shield(newDamage);

        // SetStatValue(StatName.MaxHealth, GetStatValue(StatName.MaxHealth) - damage);
        Debug.Log(gameObject.name + " just took " + newDamage + " damage.");
        GetComponent<EffectManager>().PlayParticleEffect("damage");
        CheckHealth();
    }

    public void OnDeath(float deathTimer = 0)
    {
        if (alive)
        {
            alive = false;
            GetComponent<PlayerStateMachine>().inputManager.controls.Disable();
            GameManager.instance.RestartLevel();
        }
    }

    public void Heal(float healAmount)
    {
        float MaxHealth = GetStatValue(StatName.MaxHealth);
        if (currentHealth.Value < MaxHealth)
        {
            if (currentHealth.Value + healAmount > MaxHealth)
            {
                float overflow = currentHealth.Value + healAmount - MaxHealth;
                currentHealth.Value += healAmount - overflow;
            }
            else
            {
                currentHealth.Value += healAmount;
            }
        }
    }

    public float Shield(float newDamage)
    {
        if (shieldValue)
        {
            if (shieldValue.Value > 0)
            {
                float shieldTemp = shieldValue.Value;
                shieldValue.Value -= newDamage;
                newDamage -= shieldTemp;
            }
        }



        return newDamage;
    }
}