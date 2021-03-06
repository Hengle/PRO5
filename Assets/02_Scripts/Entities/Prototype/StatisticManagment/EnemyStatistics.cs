using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatistics : StatisticController, IHasHealth
{
    public float currentHealth;
    public float skillChargeOnDeath;
    bool alive;
    protected override void InitStats()
    {
        alive = true;
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }

        currentHealth = GetStatValue(StatName.MaxHealth);
    }

    #region health
    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    //Calculates simple damage values
    public void TakeDamage(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.Defense) * GetMultValue(MultiplierName.defense))));
        // SetStatValue(StatName.MaxHealth, (GetStatValue(StatName.MaxHealth) - calcDamage));

        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
        GetComponent<EffectManager>().PlayParticleEffect("damage");
        currentHealth -= calcDamage;
        CheckHealth();
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
    }

    public void Heal(float healAmount)
    {

    }

    public void OnDeath(float deathTimer = 1f)
    {
        if (alive)
        {
            GetComponent<EffectManager>().PlayParticleEffect("stun");
            GetComponent<EffectManager>().SpawnParticleffecT("death");
            alive = false;
            MyEventSystem.instance.OnEnemyDeath(GetComponent<EnemyBody>());
            Destroy(gameObject, deathTimer);
        }
    }
    #endregion
}
