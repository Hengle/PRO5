using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyActions : MonoBehaviour
{
    [HideInInspector] public EnemyStatistics stats => GetComponent<EnemyStatistics>();
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public EnemyBody body => GetComponent<EnemyBody>();

    [Header("States")]
    /// <summary>
    /// Set this to true when an enemy needs to be stunned
    /// </summary>
    public bool isStunned;

    //List for checking if an enemy can be hit with a specific powerup e.g. very heavy enemies might not be able to be knocked back
    //or there are stun resistant enemies, etc.
    [Tooltip("List for checking if an enemy can be hit with a specific powerup e.g. very heavy enemies might not be able to be knocked back")]
    public List<PowerupNames> hittablePowerups;

    //Searching for a powerup name
    public bool FindPowerup(PowerupNames name)
    {
        return hittablePowerups != null ? hittablePowerups.Contains(name) : false;
    }
}

// namespace Powerups
// {
//     public abstract class Action : MonoBehaviour
//     {
//         public PowerupNames powerupName;
                
//         public abstract void Execute(GameObject exec);
//     }

//     public class Knockback : Action
//     {
//         public float force;
//         public override void Execute(GameObject exec)
//         {
//             RaycastHit[] hits = Physics.SphereCastAll(exec.transform.position, 3f, exec.transform.forward, 4f);
//             foreach (RaycastHit hit in hits)
//             {
//                 EnemyActions ac = hit.transform.GetComponent<EnemyActions>();
//                 if (ac != null & ac.FindPowerup(powerupName))
//                 {
//                     Vector3 direction = (ac.transform.position - exec.transform.position).normalized;
//                     ac.rb.AddForce(direction * force, ForceMode.Impulse);
//                 }
//             }
//         }
//     }

//     public class Stun : Action
//     {
//         public float stunCooldown = 1f;
//         public override void Execute(GameObject exec)
//         {
//             RaycastHit[] hits = Physics.SphereCastAll(exec.transform.position, 3f, exec.transform.forward, 4f);
//             foreach (RaycastHit hit in hits)
//             {
//                 EnemyActions ac = hit.transform.GetComponent<EnemyActions>();
//                 if (ac != null & ac.FindPowerup(powerupName))
//                 {
//                     if (!ac.isStunned)
//                         StartCoroutine(StunCooldown(ac));
//                 }
//             }
//         }

//         IEnumerator StunCooldown(EnemyActions ac)
//         {
//             ac.isStunned = true;
//             yield return new WaitForSeconds(stunCooldown);
//             ac.isStunned = false;
//         }
//     }
// }

