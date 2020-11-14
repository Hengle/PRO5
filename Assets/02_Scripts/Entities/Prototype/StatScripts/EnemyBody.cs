using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IHasHealth, IKnockback
{
    public bool isAttacking;
    public bool canAttack;
    public bool isStunned;

    public Transform rayEmitter;
    public StatTemplate statTemplate;
    public StatisticController statistics;
    public Rigidbody rb => GetComponent<Rigidbody>();
    public Coroutine stunCoroutine { get; set; }
    public float currentStun { get; set; }
    public float stunCooldown = 0.5f;
    // public Symbol symbolInfo;
    public GameObject parent;
    BehaviorExecutor behaviourExec => GetComponent<BehaviorExecutor>();
    AIManager aiManager => (AIManager)behaviourExec.blackboard.objectParams.Find(x => x.GetType().Equals(typeof(AIManager)));
    public float currentHealth;

    private void Awake()
    {
        statistics = new StatisticController();
        InitStats(statTemplate);
    }

    #region Init

    //Initializes enemy statistics
    public void InitStats(StatTemplate template)
    {
        statistics.multList = new List<Multiplier>();
        statistics.statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statistics.statList.Add(new GameStatistics(f.Value, s.statName));
        }

        currentHealth = statistics.GetStatValue(StatName.MaxHealth);
    }

    // void InitSymbol()
    // {
    //     // symbol.GetComponent<MeshRenderer>().material = symbolInfo.mat;
    //     MeshRenderer[] rend = symbol.GetComponentsInChildren<MeshRenderer>();
    //     foreach (MeshRenderer r in rend)
    //         r.material = symbolInfo.mat;
    // }

    #endregion

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
        float calcDamage = damage * statistics.GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (statistics.GetStatValue(StatName.Defense) * statistics.GetMultValue(MultiplierName.defense))));
        // SetStatValue(StatName.MaxHealth, (GetStatValue(StatName.MaxHealth) - calcDamage));

        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
        currentHealth -= calcDamage;
        CheckHealth();
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
    }

    public void Heal(float healAmount)
    {

    }

    public void OnDeath()
    {
        // MyEventSystem.instance.OnEnemyDeath(this);
        Destroy(parent);
    }
    #endregion

    //Applies a knockback force to the rigidbody into the opposite direction of the player
    public void ApplyKnockback(float force)
    {
        Vector3 direction = (transform.position - aiManager.playerTarget.position).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    //Stuns the enemy
    //Takes in a stunvalue which gets added to itself until the stunvalue is higher than the stunresistance
    public void AddStun(float stun)
    {
        if (stunCoroutine != null)
            StopCoroutine(stunCoroutine);

        stunCoroutine = StartCoroutine(StunCooldown());
        currentStun += stun;

        if (currentStun > statistics.GetStatValue(StatName.StunResist))
            isStunned = true;
    }

    //After a certain amount of time of not taking damage, stunvalue gets reduced over time
    public IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(stunCooldown);
        while (currentStun >= 0)
            currentStun -= Time.deltaTime;
    }
}
