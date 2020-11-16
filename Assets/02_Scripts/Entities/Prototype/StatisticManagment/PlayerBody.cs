using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour, IHasHealth
{
    public StatTemplate template;
    public FloatVariable currentHealth;
    public StatisticController statistics;

    public bool isDashing;
    public bool alive = true;

    private void Awake()
    {
        InitStats(template);
    }

    public void InitStats(StatTemplate template)
    {
        statistics.multList = new List<Multiplier>();
        statistics.statList = new List<GameStatistics>();
        foreach (FloatReference f in template.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statistics.statList.Add(new GameStatistics(f.Value, s.statName));
        }
        currentHealth.Value = statistics.GetStatValue(StatName.MaxHealth);
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
        //float damage = baseDmg * (baseDmg/(baseDmg + enemy.GetStat(EnemyStatName.defense)))
        float newDamage = damage * damage / (damage +  statistics.GetStatValue(StatName.Defense));
        currentHealth.Value -= newDamage;
        // SetStatValue(StatName.MaxHealth, GetStatValue(StatName.MaxHealth) - damage);
        Debug.Log(gameObject.name + " just took " + newDamage + " damage.");
        CheckHealth();
    }

    public void OnDeath()
    {
        alive = false;
        LevelEventSystem.instance.ReturnToCheckpoint(this);
    }

    public void Heal(float healAmount)
    {
        float MaxHealth =  statistics.GetStatValue(StatName.MaxHealth);
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
}
