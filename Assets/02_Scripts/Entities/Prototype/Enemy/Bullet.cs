using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool friendlyFire;
    float damage;
    Rigidbody rb => GetComponent<Rigidbody>();

    public void InitBUllet(Vector3 dir, float force, float dmg, bool _friendlyFire)
    {
        damage = dmg;
        friendlyFire = _friendlyFire;
        rb.AddForce(dir * force, ForceMode.Impulse);
        Destroy(this.gameObject, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStatistics p = other.GetComponent<PlayerStatistics>();

        if (friendlyFire)
        {
            EnemyStatistics e = other.GetComponent<EnemyStatistics>();
            if (e != null)
            {
                MyEventSystem.instance.OnAttack(e, damage);
            }
        }

        if (p != null)
        {
            MyEventSystem.instance.OnAttack(p, damage);
        }
        Destroy(gameObject);
    }
}
