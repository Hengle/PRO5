using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShentauActions : IEnemyActions
{
    public float countdown;
    public bool canAttack = false;
    public float bulletForce = 20f;
    public Animator animator;
    public GameObject bullet;
    public Transform bulletPoint;
    public Transform laser;
    public override void Init()
    {
        StartCoroutine(Recharge());
    }
    public override void Attack(int i = -1, bool combo = false)
    {
        // switch (i)
        // {
        //     case 0:
        //         Shoot(s);
        //         break;
        //     case 1:
        //         ShowChargeLaser(s);
        //         break;
        //     case 2:
        //         laser.gameObject.SetActive(true);
        //         break;
        //     case 3:
        //         laser.gameObject.SetActive(false);
        //         break;
        // }
    }

    void Shoot()
    {
        // animator.SetTrigger("attack");
        // Bullet b = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation).GetComponent<Bullet>();
        // b.InitBUllet(bulletPoint.forward, bulletForce, s.enemyStats.GetStatValue(StatName.BaseDmg));
        // StartCoroutine(Recharge());
    }
    
    void ShowChargeLaser()
    {

        RaycastHit hit;
        float dist;

        if (Physics.Raycast(bulletPoint.position, bulletPoint.forward, out hit, 20f))
            dist = hit.distance;
        else
            dist = 25f;

        laser.localScale = new Vector3(laser.localScale.x, laser.localScale.y, dist);
    }
    public override void CancelAttack()
    {
        throw new System.NotImplementedException();
    }

    public override bool CheckIsAttacking()
    {
        return canAttack;
    }

    public override float GetAttackCountdown()
    {
        return countdown;
    }

    public override void StopAttack()
    {

    }

    public override void StopWalking()
    {

    }

    public override void Stunned()
    {

    }

    public override void Walk()
    {

    }

    IEnumerator Recharge()
    {
        // canAttack = false;
        // if (s == null)
        //     countdown = 6f;
        // else
        //     countdown = s.enemyStats.GetStatValue(StatName.AttackRate);

        // while (countdown >= 0)
        // {
            yield return new WaitForSeconds(0.1f);
        //     countdown -= 0.1f;
        // }
        // canAttack = true;
    }

    public override Animator GetAnimator()
    {
        return animator;
    }
}
