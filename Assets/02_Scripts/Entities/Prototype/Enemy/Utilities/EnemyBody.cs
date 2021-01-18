using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public Transform rayEmitter;
    public BoxCollider hitBox;
    public PlayerDetector playerDetector;
    public AIManager aiManager;
    private void Awake()
    {
        aiManager = ScriptCollection.GetScript<AIManager>();
    }
}
