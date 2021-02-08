using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class EnemyActions : MonoBehaviour
{
    [HideInInspector] public EnemyStatistics stats => GetComponent<EnemyStatistics>();
    [HideInInspector] public Rigidbody rb => GetComponent<Rigidbody>();
    [HideInInspector] public EnemyBody body => GetComponent<EnemyBody>();
    BehaviorExecutor executor => GetComponent<BehaviorExecutor>();
    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    [Header("States")]
    /// <summary>
    /// Set this to true when an enemy needs to be stunned
    /// </summary>
    public bool isStunned;

    public void Stun()
    {
        // GetComponent<AnimatorHook>().animator.SetBool("isStunned", true);
        GetComponent<EffectManager>().PlayParticleEffect("stun");
        GetComponent<EffectManager>().PlaySoundEffect("stun");
        executor.paused = true;
        // agent.isStopped = true;
        agent.enabled = false;
    }

    public void UnStun()
    {
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, 5f, LayerMask.GetMask("Floor"))
            || Physics.Raycast(gameObject.transform.position + new Vector3(0, 0, -0.5f), Vector3.down, 5f, LayerMask.GetMask("Floor")))
        {
            // GetComponent<AnimatorHook>().animator.SetBool("isStunned", false);
            GetComponent<EffectManager>().StopParticleEffect("stun");
            GetComponent<EffectManager>().StopSoundEffect("stun");
            agent.enabled = true;
            // agent.isStopped = false;
            executor.paused = false;
        }
        else
        {
            ScriptCollection.GetScript<AIUtilities>().DestroyObject(this.gameObject, 3f);
        }
    }
    //List for checking if an enemy can be hit with a specific powerup e.g. very heavy enemies might not be able to be knocked back
    //or there are stun resistant enemies, etc.
    [Tooltip("List for checking if an enemy can be hit with a specific powerup e.g. very heavy enemies might not be able to be knocked back")]
    public List<PowerupNames> hittablePowerups;

    //Searching for a powerup name
    public bool FindPowerup(PowerupNames name)
    {
        return hittablePowerups != null ? hittablePowerups.Contains(name) : false;
    }

    // private void OnDrawGizmos()
    // {
    //     RaycastHit tempRayWallHit;
    //     float deg = 50;
    //     float dot;
    //     float dot2;
    //     float currentAngle = 0;
    //     Vector3 dir = Vector3.zero;
    //     for (int i = 0; i < deg; i++)
    //     {
    //         currentAngle += 360f / deg;

    //         float y = Mathf.Sin(Mathf.Deg2Rad + currentAngle);
    //         float x = Mathf.Cos(Mathf.Deg2Rad + currentAngle);
    //         Vector3 pos = new Vector3(x, 0, y) * 4f + gameObject.transform.position;

    //         Vector3 cornerDir = Vector3.zero;

    //         int count = 0;
    //         if (Physics.Raycast(gameObject.transform.position, Vector3.right, out tempRayWallHit, 3f, LayerMask.GetMask("Wall")))
    //         {
    //             count++;
    //             cornerDir += Vector3.right / 2;
    //         }

    //         if (Physics.Raycast(gameObject.transform.position, -Vector3.right, out tempRayWallHit, 3f, LayerMask.GetMask("Wall")))
    //         {
    //             count++;
    //             cornerDir -= Vector3.right / 2;
    //         }

    //         if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out tempRayWallHit, 3f, LayerMask.GetMask("Wall")))
    //         {
    //             count++;
    //             cornerDir += Vector3.forward / 2;
    //         }

    //         if (Physics.Raycast(gameObject.transform.position, -Vector3.forward, out tempRayWallHit, 3f, LayerMask.GetMask("Wall")))
    //         {
    //             count++;
    //             cornerDir -= Vector3.forward / 2;
    //         }


    //         if (count == 1)
    //         {
    //             // if (Physics.Raycast(gameObject.transform.position, (pos - gameObject.transform.position).normalized, out rayHit, 8f, LayerMask.GetMask("Wall")))
    //             // {
    //             dot = Vector3.Dot((body.aiManager.playerTarget.position - gameObject.transform.position).normalized, (pos - gameObject.transform.position).normalized);
    //             dot2 = Vector3.Dot(cornerDir.normalized, (pos - gameObject.transform.position).normalized);
    //             if (dot2 <= 0.2f && dot <= 0.6f)
    //             {
    //                 Gizmos.color = Color.blue;
    //                 Gizmos.DrawLine(gameObject.transform.position, pos);
    //             }
    //             // dot = Vector3.Dot((pos - gameObject.transform.position).normalized, (body.aiManager.playerTarget.position - gameObject.transform.position).normalized);
    //             // if (dot < 0.5f)
    //             // {
    //             //     Gizmos.DrawLine(gameObject.transform.position, pos);
    //             // }
    //             // }
    //         }
    //         else if (count == 2)
    //         {
    //             // if (Physics.Raycast(gameObject.transform.position, cornerDir.normalized, out rayCornerHit, 6f, LayerMask.GetMask("Wall")))
    //             // {
    //             dot = Vector3.Dot(cornerDir.normalized,
    //                                 (pos - gameObject.transform.position).normalized);
    //             if (CheckAngle(dot, -0.5f))
    //             {
    //                 Gizmos.color = Color.blue;
    //                 Gizmos.DrawLine(gameObject.transform.position, pos);
    //             }
    //             // }
    //         }
    //         else
    //         {
    //             dot = Vector3.Dot((pos - gameObject.transform.position).normalized, (body.aiManager.playerTarget.position - gameObject.transform.position).normalized);
    //             if (dot < 0.6f)
    //             {
    //                 Gizmos.color = Color.white;
    //                 Gizmos.DrawLine(gameObject.transform.position, pos);
    //             }
    //         }
    //     }
    // }

    // bool CheckAngle(float dot, float angle)
    // {
    //     // if (angle >= 0)
    //     // {
    //     return dot < angle;
    //     // }
    //     // else
    //     // {
    //     //     return dot > angle;
    //     // }
    // }
}

#region stuff
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
#endregion
