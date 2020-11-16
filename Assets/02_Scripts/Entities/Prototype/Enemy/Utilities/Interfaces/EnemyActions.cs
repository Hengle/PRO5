using UnityEngine;
using System.Collections;
public class EnemyActions : MonoBehaviour
{
    public bool isStunned;
    public bool isAttacking;
    public bool canAttack;
    public bool canDamage;
    [SerializeField] float stunCooldown = 0.5f;
    float currentStun;

    EnemyStatistics stats => GetComponent<EnemyStatistics>();
    Rigidbody rb => GetComponent<Rigidbody>();
    EnemyBody body => GetComponent<EnemyBody>();
    Coroutine stunCoroutine;

    //Pushes the Enemy into the opposite direction of the player
    public void ApplyKnockback(float force)
    {
        Vector3 direction = (transform.position - body.aiManager.playerTarget.position).normalized;
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

        if (currentStun > stats.GetStatValue(StatName.StunResist))
            isStunned = true;
    }

    //After a certain amount of time of not taking damage, stunvalue gets reduced over time
    IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(stunCooldown);
        while (currentStun >= 0)
            currentStun -= Time.deltaTime;
    }
}
