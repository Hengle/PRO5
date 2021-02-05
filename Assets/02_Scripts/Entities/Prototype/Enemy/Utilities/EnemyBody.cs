using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public Transform rayEmitter;
    public BoxCollider hitBox;
    public PlayerDetector playerDetector;
    public AIManager aiManager;

    private void Start()
    {
        MyEventSystem.instance.OnEnemyStart(this);
        aiManager = ScriptCollection.GetScript<AIManager>();
    }
}
