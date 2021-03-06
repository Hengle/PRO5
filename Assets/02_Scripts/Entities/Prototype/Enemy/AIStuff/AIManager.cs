using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public string searchPlayerTag = "Player";
    public EnemySet enemySet;
    [HideInInspector] public LayerMask groundMask => LayerMask.GetMask("Ground");
    [HideInInspector] public LayerMask enemyMask => LayerMask.GetMask("Enemy");
    [HideInInspector] public LayerMask playerMask => LayerMask.GetMask("Player");
    public Transform playerTarget;

    [Header("Avoid settings")]
    public float avoidDistance = 5f;
    public float obstacleAvoidDistance = 2f;
    public float mainWhiskerL = 2f;
    public float secondaryWhiskerL = 1.5f;
    public float angleIncrement = 10f;
    public int whiskerAmount = 11;

    public float activationTime = 0.2f;
    private void Start()
    {
        // allSet.entityList = new List<EnemyBody>();
    }

    private void OnEnable()
    {
        ScriptCollection.RegisterScript(this);
    }
    private void OnDisable()
    {
        ScriptCollection.RemoveScript(this);
    }

    public void SetAIActive(EnemyBody enemy)
    {

    }

    private void Update()
    {
        // if (allSet.entityList.Count == 0)
        //     return;

        // GetCircleOffstets();
    }

    // void GetCircleOffstets()
    // {
    //     float currentAngle = 0;
    //     playerOffsetList = new Vector3[allSet.entityList.Count];
    //     foreach (EnemyBody enemy in allSet.entityList)
    //     {
    //         StateMachineController st = enemy.GetComponent<StateMachineController>();
    //         float x = 0;
    //         float z = 0;
    //         currentAngle += 360f / allSet.entityList.Count;
    //         float convAngle = currentAngle * Mathf.PI / 180F;
    //         x = (float)(4f * Mathf.Cos(convAngle) + playerTarget.position.x);
    //         z = (float)(4f * Mathf.Sin(convAngle) + playerTarget.position.z);
    //         st.offsetTargetPos = new Vector3(x, playerTarget.position.y, z);
    //     }
    // }

    // private void OnDrawGizmos()
    // {
    //     if (set.entityList.Count == 0 || set.entityList == null)
    //         return;

    //     foreach (Vector3 vec in playerOffsetList)
    //     {
    //         Gizmos.DrawWireSphere(vec, 0.2f);
    //     }
    // }

    public void FindPlayer()
    {
        if (FindObjectOfType<PlayerBody>() != null)
            playerTarget = FindObjectOfType<PlayerBody>().GetComponent<Transform>();
        else if (GameObject.FindGameObjectWithTag(searchPlayerTag) != null)
            playerTarget = GameObject.FindGameObjectWithTag(searchPlayerTag).GetComponent<Transform>();
        else
            Debug.LogError("Could not find Player!");
    }
}

